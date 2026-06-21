using Newtonsoft.Json.Linq;

namespace MSMPSharp.Core;

public class MsmpException(string message, int code, JToken? data = null) : Exception(message)
{
    public int Code { get; } = code;
    public new JToken? Data { get; } = data;
}