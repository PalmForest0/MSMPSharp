using Newtonsoft.Json.Linq;

namespace MSMPSharp.Data.RPC;

public sealed class RpcError
{
    public int Code { get; init; }
    public string Message { get; init; } = "";
    public JToken? Data { get; init; }
}