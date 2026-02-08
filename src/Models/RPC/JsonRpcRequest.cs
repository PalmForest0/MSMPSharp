namespace MSMPSharp.Models.RPC;

public class JsonRpcRequest
{
    public string JsonRpc { get; set; } = "2.0";
    public string Method { get; set; } = "";
    public object[] Params { get; set; } = [];
    public int Id { get; set; }
}