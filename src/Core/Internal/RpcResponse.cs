using Newtonsoft.Json.Linq;

namespace MSMPSharp.Core.Internal;

internal sealed class RpcResponse
{
    public string Jsonrpc { get; init; } = "2.0";
    public int Id { get; init; }
    public JToken? Result { get; init; }
    public RpcError? Error { get; init; }
}