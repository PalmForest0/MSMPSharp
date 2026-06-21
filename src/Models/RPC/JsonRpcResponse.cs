using Newtonsoft.Json.Linq;

namespace MSMPSharp.Data.RPC;

public sealed class JsonRpcResponse
{
    public string Jsonrpc { get; init; } = "2.0";
    public int Id { get; init; }
    public JToken? Result { get; init; }
    public RpcError? Error { get; init; }
}