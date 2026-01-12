namespace MSMPSharp.Models.Server;

public class IpBan
{
    public string Ip { get; set; }
    public string Reason { get; set; }
    public string Expires { get; set; }
    public string Source { get; set; }
}