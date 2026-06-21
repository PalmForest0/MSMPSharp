using MSMPSharp.Models.Game;
using MSMPSharp.Extensions;

namespace MSMPSharp.Models.Server;

public class UserBan
{
    public string? Reason { get; set; }
    public string? Expires { get; set; }
    public string? Source { get; set; }
    public Player Player { get; set; }

    /// <summary>
    /// Defines the data of a user ban for a specific player.
    /// </summary>
    /// <param name="player">The player this ban applies to.</param>
    /// <param name="reason">Optional reason for this ip ban.</param>
    /// <param name="source">Optional source for this ip ban.</param>
    /// <param name="expires">Optional expiry DateTime of this ip ban.</param>
    public UserBan(Player player, DateTime? expires = null, string? reason = null, string? source = null)
    {
        Player = player;
        Expires = expires?.ToMCString();

        Reason = reason;
        Source = source;
    }
}