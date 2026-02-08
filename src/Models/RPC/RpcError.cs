namespace MSMPSharp.Models.RPC;

public sealed class RpcError
{
    public int Code { get; set; }
    public string Message { get; set; } = "";
    public object? Data { get; set; }
}