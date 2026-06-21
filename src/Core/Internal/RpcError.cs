using Newtonsoft.Json.Linq;

namespace MSMPSharp.Core.Internal;

internal sealed class RpcError
{
    public int Code { get; init; }
    public string Message { get; init; } = "";
    public JToken? Data { get; init; }
}