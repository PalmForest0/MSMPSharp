using Newtonsoft.Json.Linq;

namespace MSMPSharp.Models.RPC;

public class JsonRpcResponse
{
    public string Jsonrpc { get; set; } = "";
    public int Id { get; set; }
    public JToken? Result { get; set; }
    public RpcError? Error { get; set; }
}