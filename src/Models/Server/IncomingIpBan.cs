using MSMPSharp.Models.Game;
using MSMPSharp.Extensions;

namespace MSMPSharp.Models.Server;

public sealed class IncomingIpBan
{
    public Player? Player { get; }
    public string? Ip { get; }
    public string? Reason { get; }
    public string? Expires { get; }
    public string? Source { get; }


    private IncomingIpBan(Player? player, string? ip, string? reason, string? expires, string? source)
    {
        Player = player;
        Ip = ip;
        Reason = reason;
        Expires = expires;
        Source = source;
    }

    /// <summary>
    /// Defines an Ip ban to be sent to the server for a specific player.
    /// </summary>
    /// <param name="player">Player that this ip ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="expires">Optional expiry DateTime of this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    public static IncomingIpBan ToPlayer(Player player, string? reason = null, DateTime? expires = null, string? source = null)
        => new(player, ip: null, reason, expires?.ToMCString(), source);

    ///// <summary>
    ///// Defines an Ip ban to be sent to the server for a specific player.
    ///// </summary>
    ///// <param name="player">Player that this ip ban applies to.</param>
    ///// <param name="reason">Optional reason for this ip ban.</param>
    ///// <param name="expires">Optional expiry string of this ip ban. For automatic formatting <c>("yyyy-MM-ddTHH:mm:ssZ")</c>, use <see cref="ToPlayer(Player, string?, DateTime?, string?)"/></param>
    ///// <param name="source">Optional source for this ip ban.</param>
    //public static IncomingIpBan ToPlayer(Player player, string? reason = null, string? expires = null, string? source = null)
    //    => new(player, ip: null, reason, expires, source);


    /// <summary>
    /// Defines an Ip ban to be sent to the server for a specific player.
    /// </summary>
    /// <param name="ip">The Ip address this ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="expires">Optional expiry DateTime of this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    public static IncomingIpBan ToIp(string ip, string? reason = null, DateTime? expires = null, string? source = null)
        => new(player: null, ip, reason, expires?.ToMCString(), source);

    ///// <summary>
    ///// Defines an Ip ban to be sent to the server for a specific player.
    ///// </summary>
    ///// <param name="ip">The Ip address this ban applies to.</param>
    ///// <param name="reason">Optional reason for this ip ban.</param>
    ///// <param name="source">Optional source for this ip ban.</param>
    ///// <param name="expires">Optional expiry string of this ip ban. For automatic formatting <c>("yyyy-MM-ddTHH:mm:ssZ")</c>, use <see cref="ToIp(string, string?, DateTime?, string?)"/></param>
    //public static IncomingIpBan ToIp(string ip, string? reason = null, string? expires = null, string? source = null)
    //    => new(player: null, ip, reason, expires, source);

    /// <summary>
    /// Create an incoming ip ban from an existing ip ban.
    /// </summary>
    /// <param name="ban">Existing ip ban.</param>
    public static implicit operator IncomingIpBan(IpBan ban) => new(player: null, ban.Ip, ban.Reason, ban.Expires, ban.Source);
}