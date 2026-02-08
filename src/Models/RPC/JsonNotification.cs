using Newtonsoft.Json.Linq;
using System.Collections;

namespace MSMPSharp.Models.RPC;

public sealed class JsonNotification
{
    public string Jsonrpc { get; set; } = "2.0";
    public required string Method { get; set; }
    public required JToken Params { get; set; }

    public T? GetParams<T>() => Params.ToObject<T>();
    public bool TryGetParams<T>(out T? result)
    {
        var res = Params.ToObject<T>();

        if(res is null || (res is ICollection col && col.Count == 0))
        {
            result = default;
            return false;
        }

        result = res;
        return true;
    }
}