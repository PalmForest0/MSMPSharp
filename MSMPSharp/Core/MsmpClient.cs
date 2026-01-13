using MSMPSharp.Extensions;
using MSMPSharp.Models.RPC;
using MSMPSharp.Modules;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.WebSockets;
using System.Text;

namespace MSMPSharp.Core;

public class MsmpClient
{
    private static readonly JsonSerializerSettings _jsonSettings = new() { ContractResolver = new DefaultContractResolver { NamingStrategy = new LowerCaseNamingStrategy() } };

    private readonly Uri _serverUri;
    private readonly ClientWebSocket _socket;

    private int _latestRequestId = 1;

    public PlayersModule Players { get; }
    public AllowlistModule Allowlist { get; }
    public BansModule Bans { get; }
    public IpBansModule IpBans { get; }
    public OperatorsModule Operators { get; }
    public ServerModule Server { get; }
    public GamerulesModule Gamerules { get; }

    public MsmpClient(string host, int port, string secret)
    {
        _serverUri = new Uri($"ws://{host}:{port}");
        _socket = new ClientWebSocket();
        _socket.Options.SetRequestHeader("Authorization", $"Bearer {secret}");

        Players = new PlayersModule(this);
        Allowlist = new AllowlistModule(this);
        Bans = new BansModule(this);
        IpBans = new IpBansModule(this);
        Operators = new OperatorsModule(this);
        Server = new ServerModule(this);
        Gamerules = new GamerulesModule(this);
    }

    /// <summary>
    /// Connects to the Minecraft server through the websocket.
    /// </summary>
    public async Task ConnectAsync() => await _socket.ConnectAsync(_serverUri, CancellationToken.None);

    public async Task DisconnectAsync()
    {
        await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Success.", CancellationToken.None);
        _socket.Dispose();
    }

    /// <summary>
    /// Sends an RPC request as JSON to the Minecraft server through the websocket.
    /// </summary>
    private async Task SendRequestAsync(JsonRpcRequest request)
    {
        // Custom JSON setting required to convert all property names to lowercase
        string json = JsonConvert.SerializeObject(request, _jsonSettings);
        var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));

        await _socket.SendAsync(buffer, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
    }

    /// <summary>
    /// Receives a JSON RPC response and returns the result as the specified type.
    /// </summary>
    /// <typeparam name="T">The expected type of the response result.</typeparam>
    private async Task<T> ReceiveResponseAsync<T>()
    {
        string json = await _socket.ReceiveInChunksAsync(CancellationToken.None);
        var response = JsonConvert.DeserializeObject<JsonRpcResponse>(json) ?? throw new InvalidOperationException("Invalid JSON-RPC response.");

        if (response.Error is not null)
            throw new WebSocketException($"{response.Error.Message} ({response.Error.Code})\n\"{response.Error.Data}\"");

        if (response.Result is null)
            throw new InvalidOperationException("Result is missing from the response.");

        return response.Result.ToObject<T>()!;
    }

    public async Task<T> CallMethodAsync<T>(string method) => await CallMethodAsync<T>(method, Array.Empty<object>());

    public async Task<T> CallMethodAsync<T>(string method, object[] parameters)
    {
        await SendRequestAsync(new JsonRpcRequest
        {
            Method = method,
            Params = parameters,
            Id = _latestRequestId++
        });

        return await ReceiveResponseAsync<T>();
    }
}