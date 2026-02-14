using Newtonsoft.Json.Linq;

namespace MSMPSharp.Data.RPC;

public sealed class JsonRpcResponse
{
    public string Jsonrpc { get; set; } = "2.0";
    public int Id { get; set; }
    public JToken? Result { get; set; }
    public RpcError? Error { get; set; }
}