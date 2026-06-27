using System.Text.Json.Nodes;

namespace MSMPSharp.Core.Internal;

internal sealed class RpcError
{
    public int Code { get; init; }
    public string Message { get; init; } = "";
    public JsonObject? Data { get; init; }
}