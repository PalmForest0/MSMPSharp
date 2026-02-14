using MSMPSharp.Data.Game;

namespace MSMPSharp.Data.Server;

public class UserBan
{
    public string Reason { get; set; }
    public string Expires { get; set; }
    public string Source { get; set; }
    public Player Player { get; set; }
}