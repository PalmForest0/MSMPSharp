using MSMPSharp.Data.Game;

namespace MSMPSharp.Data.Server;

public class IncomingIpBan
{
    public string? Ip { get; set; }
    public string Reason { get; set; }
    public string Expires { get; set; }
    public string Source { get; set; }
    public Player? Player { get; set; }

    /// <summary>
    /// Defines an Ip ban to be sent to the server for a specific player.
    /// </summary>
    /// <param name="player">Player that this ip ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    /// <param name="expires">Optional expiry of this ip ban.</param>
    public IncomingIpBan(Player player, string reason = "", string source = "", string expires = "")
    {
        Player = player;
        Reason = reason;
        Source = source;
        Expires = expires;
    }

    /// <summary>
    /// Defines an Ip ban to be sent to the server for a specific player.
    /// </summary>
    /// <param name="ip">The Ip address this ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    /// <param name="expires">Optional expiry of this ip ban.</param>
    public IncomingIpBan(string ip, string reason = "", string source = "", string expires = "")
    {
        Ip = ip;
        Reason = reason;
        Source = source;
        Expires = expires;
    }
}