namespace MSMPSharp.Models.RPC;

public class JsonRpcResponse<T>
{
    public string Jsonrpc { get; set; } = "";
    public int Id { get; set; }
    public T? Result { get; set; }
    public RpcError? Error { get; set; }
}