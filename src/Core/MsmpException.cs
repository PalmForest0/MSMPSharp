using System.Text.Json.Nodes;

namespace MSMPSharp.Core;

public class MsmpException(string message, int code, JsonNode? data = null) : Exception(message)
{
    public int Code { get; } = code;
    public new JsonNode? Data { get; } = data;
}