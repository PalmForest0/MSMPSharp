using MSMPSharp.Extensions;
using MSMPSharp.Models.RPC;
using MSMPSharp.Modules;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.WebSockets;
using System.Text;

namespace MSMPSharp.Core;

public class MsmpClient(string host, int port)
{
    private readonly Uri _serverUri = new Uri($"ws://{host}:{port}");
    private readonly ClientWebSocket _socket = new();

    private int _requestId = 0;

    private readonly JsonSerializerSettings _jsonSettings = new()
    {
        ContractResolver = new DefaultContractResolver { NamingStrategy = new LowerCaseNamingStrategy() }
    };

    public async Task ConnectAsync() => await _socket.ConnectAsync(_serverUri, CancellationToken.None);

    private async Task SendRequestAsync(JsonRpcRequest request)
    {
        string json = JsonConvert.SerializeObject(request, _jsonSettings);
        var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));
        await _socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
    }

    private async Task<T> ReceiveResponseAsync<T>()
    {
        string json = await _socket.ReceiveInChunksAsync(CancellationToken.None);
        JsonRpcResponse<T>? response = JsonConvert.DeserializeObject<JsonRpcResponse<T>>(json) ?? throw new InvalidOperationException("Invalid JSON-RPC response");

        if (response.Error is not null)
            throw new WebSocketException($"{response.Error.Message} ({response.Error.Code})\n\"{response.Error.Data}\"");

        if (response.Result is null)
            throw new InvalidOperationException("Missing result in response.");

        return response.Result;
    }

    public async Task<T> CallMethodAsync<T>(string method) => await CallMethodAsync<T>(method, Array.Empty<object>());

    public async Task<T> CallMethodAsync<T>(string method, params object[] parameters)
    {
        await SendRequestAsync(new JsonRpcRequest
        {
            Method = method,
            Params = parameters,
            Id = _requestId++
        });

        return await ReceiveResponseAsync<T>();
    }
}