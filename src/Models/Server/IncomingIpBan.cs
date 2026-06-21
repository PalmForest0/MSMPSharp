using MSMPSharp.Models.Game;
using MSMPSharp.Extensions;

namespace MSMPSharp.Models.Server;

public class IncomingIpBan
{
    public Player? Player { get; set; }
    public string? Ip { get; set; }

    public string? Reason { get; set; }
    public string? Expires { get; set; }
    public string? Source { get; set; }

    /// <summary>
    /// Defines an Ip ban to be sent to the server for a specific player.
    /// </summary>
    /// <param name="player">Player that this ip ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    /// <param name="expires">Optional expiry DateTime of this ip ban.</param>
    public IncomingIpBan(Player player, DateTime? expires = null, string? reason = null, string? source = null)
    {
        Player = player;
        Expires = expires?.ToMCString();

        Reason = reason;
        Source = source;
    }

    /// <summary>
    /// Defines an Ip ban to be sent to the server for a specific player.
    /// </summary>
    /// <param name="ip">The Ip address this ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    /// <param name="expires">Optional expiry DateTime of this ip ban.</param>
    public IncomingIpBan(string ip, DateTime? expires = null, string? reason = null, string? source = null)
    {
        Ip = ip;
        Expires = expires?.ToMCString();

        Reason = reason;
        Source = source;
    }

    /// <summary>
    /// Create an incoming ip ban from an existing ip ban.
    /// </summary>
    /// <param name="ban">Existing ip ban.</param>
    public static implicit operator IncomingIpBan(IpBan ban) => new IncomingIpBan(ban.Ip, null, ban.Reason, ban.Source)
    {
        Expires = ban.Expires // Constructor only accepts DateTime
    };
}