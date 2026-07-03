using MSMPSharp.Models.Game;
using MSMPSharp.Extensions;

namespace MSMPSharp.Models.Server;

public sealed class UserBan
{
    public Player Player { get; }
    public string? Reason { get; }
    public string? Expires { get; }
    public string? Source { get; }


    /// <summary>
    /// Defines the data of a user ban for a specific player.
    /// </summary>
    /// <param name="player">The player this ban applies to.</param>
    /// <param name="reason">Optional reason for this user ban.</param>
    /// <param name="expires">Optional expiry DateTime of this user ban.</param>
    /// <param name="source">Optional source for this user ban.</param>
    public UserBan(Player player, string? reason = null, DateTime? expires = null, string? source = null)
    {
        Player = player;
        Reason = reason;
        Expires = expires?.ToMCString();
        Source = source;
    }
}