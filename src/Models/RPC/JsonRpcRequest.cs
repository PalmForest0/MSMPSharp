namespace MSMPSharp.Models.RPC;

public sealed class JsonRpcRequest
{
    public string JsonRpc { get; set; } = "2.0";
    public required string Method { get; set; }
    public object[] Params { get; set; } = [];
    public int Id { get; set; }
}