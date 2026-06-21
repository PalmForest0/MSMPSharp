using MSMPSharp.Extensions;
using MSMPSharp.Data.RPC;
using MSMPSharp.Modules;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Net.WebSockets;
using System.Text;
using System.Net.Security;

namespace MSMPSharp.Core;

public class MsmpClient : IAsyncDisposable
{
    private static readonly JsonSerializerSettings _jsonSettings = new() 
    { 
        ContractResolver = new DefaultContractResolver { NamingStrategy = new LowerCaseNamingStrategy() },
        NullValueHandling = NullValueHandling.Ignore
    };

    private readonly Uri _serverUri;
    private readonly ClientWebSocket _socket;

    private readonly Dictionary<string, Action<JsonRpcNotification>> _notificationEvents = new();
    private readonly Dictionary<int, TaskCompletionSource<JsonRpcResponse>> _pendingRequests = new();
    private readonly Lock _requestsLock = new();
    private int _latestRequestId = 0;

    // All modules available within the client
    public PlayersModule Players { get; }
    public AllowlistModule Allowlist { get; }
    public BansModule Bans { get; }
    public IpBansModule IpBans { get; }
    public OperatorsModule Operators { get; }
    public ServerModule Server { get; }
    public GameRulesModule GameRules { get; }
    public ServerSettingsModule ServerSettings { get; }

    // Client events
    public event EventHandler? OnConnected;
    public event EventHandler? OnDisconnected;

    public static MsmpClientBuilder CreateBuilder() => new MsmpClientBuilder();

    internal MsmpClient(string host, int port, string secret, bool useTls, string? origin, RemoteCertificateValidationCallback? certValidator)
    {
        _serverUri = new Uri($"{(useTls ? "wss" : "ws")}://{host}:{port}");
        _socket = new ClientWebSocket();
        _socket.Options.SetRequestHeader("Authorization", $"Bearer {secret}");

        if (origin is not null)
            _socket.Options.SetRequestHeader("Origin", origin);

        if (certValidator is not null)
            _socket.Options.RemoteCertificateValidationCallback = certValidator;

        Players = new PlayersModule(this);
        Allowlist = new AllowlistModule(this);
        Bans = new BansModule(this);
        IpBans = new IpBansModule(this);
        Operators = new OperatorsModule(this);
        Server = new ServerModule(this);
        GameRules = new GameRulesModule(this);
        ServerSettings = new ServerSettingsModule(this);
    }

    /// <summary>
    /// Connects to the Minecraft server through the websocket.
    /// </summary>
    public async Task ConnectAsync()
    {
        await _socket.ConnectAsync(_serverUri, CancellationToken.None);

        OnConnected?.Invoke(this, EventArgs.Empty);

        // Start a receive loop on a second thread
        _ = Task.Run(ReceiveLoopAsync);
    }

    /// <summary>
    /// Disconnects from the Minecraft server and disposes the websocket connection.
    /// </summary>
    public async Task DisconnectAsync()
    {
        if(_socket.State != WebSocketState.Open)
            return;

        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Success.", CancellationToken.None);
        OnDisconnected?.Invoke(this, EventArgs.Empty);
    }

    private async Task ReceiveLoopAsync()
    {
        try
        {
            while (_socket.State == WebSocketState.Open)
            {
                string json = await _socket.ReceiveInChunksAsync(CancellationToken.None);
                var obj = JObject.Parse(json);

                if (obj is null)
                    continue;

                // If the json has an id, it is a jobj
                if (obj["id"] is not null)
                    HandleResponse(obj);
                // If the json has a method, it is a notification
                else if (obj["method"] is not null)
                    HandleNotification(obj);
            }
        }
        finally
        {
            await DisconnectAsync();
        }
    }

    private void HandleResponse(JObject jobj)
    {
        var response = jobj.ToObject<JsonRpcResponse>();

        if (response is null)
            return;

        // Find the task that corresponds to this jobj id
        TaskCompletionSource<JsonRpcResponse>? tcs;
        lock (_requestsLock)
        {
            if (!_pendingRequests.TryGetValue(response.Id, out tcs))
                return;

            _pendingRequests.Remove(response.Id);
        }

        // Set the result of the task to this response, or set an exception if there is an error
        if (response.Error is not null)
        {
            tcs.SetException(new WebSocketException($"{response.Error.Message} ({response.Error.Code})\n\"{response.Error.Data}\""));
        }
        else
        {
            tcs.SetResult(response);
        }
    }

    private void HandleNotification(JObject jobj)
    {
        var notif = jobj.ToObject<JsonRpcNotification>();

        if (notif is null)
            return;
        if (notif.Method is null)
            return;

        // Call handler event for the method notification
        if (_notificationEvents.TryGetValue(notif.Method, out var handler))
            handler.Invoke(notif);
    }

    internal void SetNotificationEvent(string method, Action<JsonRpcNotification> handler) => _notificationEvents[method] = handler;

    /// <summary>
    /// Sends an RPC request as JSON to the Minecraft server through the websocket.
    /// </summary>
    /// <param name="request">The JSON-RPC request to send.</param>
    private async Task SendRequestAsync(JsonRpcRequest request)
    {
        if (_socket.State != WebSocketState.Open)
            return;

        // Custom JSON setting required to convert all property names to lowercase
        string json = JsonConvert.SerializeObject(request, _jsonSettings);
        var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));
        await _socket.SendAsync(buffer, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
    }

    /// <summary>
    /// Calls a JSON-RPC method on the Minecraft server with no parameters.
    /// </summary>
    /// <typeparam name="T">The expected type of the jobj result.</typeparam>
    /// <param name="method">The name of the method to call.</param>
    /// <returns>The deserialized result of type <typeparamref name="T"/>.</returns>
    public async Task<T> CallMethodAsync<T>(string method) => await CallMethodAsync<T>(method, Array.Empty<object>());

    /// <summary>
    /// Calls a JSON-RPC method on the Minecraft server with the specified parameters.
    /// </summary>
    /// <typeparam name="T">The expected type of the jobj result.</typeparam>
    /// <param name="method">The name of the method to call.</param>
    /// <param name="parameters">The parameters to pass to the method.</param>
    /// <returns>The deserialized result of type <typeparamref name="T"/>.</returns>
    public async Task<T> CallMethodAsync<T>(string method, object[] parameters)
    {
        await SendRequestAsync(new JsonRpcRequest
        {
            Method = method,
            Params = parameters,
            Id = Interlocked.Increment(ref _latestRequestId)
        });

        // Create TaskCompletionSource
        var tcs = new TaskCompletionSource<JsonRpcResponse>(TaskCreationOptions.RunContinuationsAsynchronously);

        // Add it with this request's id
        lock (_requestsLock)
        {
            _pendingRequests.Add(_latestRequestId, tcs);
        }

        // Await until the task result is set by the receiver
        var response = await tcs.Task;
        var result = response.Result!.ToObject<T>();

        if (result is null)
            throw new InvalidOperationException($"Failed to deserialize result to type {typeof(T).Name}.");

        return result;
    }

    public async Task<JObject> GetSchemaAsync() => await CallMethodAsync<JObject>("rpc.discover");

    /// <summary>
    /// Asynchronously disposes the client and closes the websocket connection.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisconnectAsync();
        _socket.Dispose();
    }
}