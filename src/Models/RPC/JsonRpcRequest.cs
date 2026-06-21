namespace MSMPSharp.Data.RPC;

public sealed class JsonRpcRequest
{
    public string JsonRpc { get; init; } = "2.0";
    public required string Method { get; init; }
    public object[] Params { get; init; } = [];
    public int Id { get; init; }
}