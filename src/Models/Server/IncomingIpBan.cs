using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

public class IncomingIpBan
{
    public string Ip { get; set; }
    public string Reason { get; set; }
    public string Expires { get; set; }
    public string Source { get; set; }
    public Player Player { get; set; }
}