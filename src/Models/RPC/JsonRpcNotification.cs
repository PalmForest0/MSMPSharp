using Newtonsoft.Json.Linq;
using System.Collections;

namespace MSMPSharp.Data.RPC;

public sealed class JsonRpcNotification
{
    public string Jsonrpc { get; init; } = "2.0";
    public required string Method { get; init; }
    public required JToken? Params { get; init; }

    public T? GetParams<T>()
    {
        if (Params is null)
            return default;

        return Params.ToObject<T>();
    }

    public bool TryGetParams<T>(out T? result)
    {
        if(Params is null)
        {
            result = default;
            return false;
        }

        result = Params.ToObject<T>();
        return result is not null;
    }
}