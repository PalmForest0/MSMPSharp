using System.Text.Json;
using System.Text.Json.Nodes;

namespace MSMPSharp.Core.Internal;

internal sealed class RpcNotification
{
    public string Jsonrpc { get; init; } = "2.0";
    public required string Method { get; init; }
    public required JsonNode? Params { get; init; }

    public T? GetParams<T>()
    {
        if (Params is null)
            return default;

        return Params.Deserialize<T>();
    }

    public bool TryGetParams<T>(out T? result)
    {
        if(Params is null)
        {
            result = default;
            return false;
        }

        result = Params.Deserialize<T>();
        return result is not null;
    }
}