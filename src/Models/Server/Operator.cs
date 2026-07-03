using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

public sealed class Operator
{
    public Player Player { get; }
    public int PermissionLevel { get; }
    public bool BypassesPlayerLimit { get; }

    /// <summary>
    /// Creates the data for a server operator using a player.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="permissionLevel"></param>
    /// <param name="bypassesPlayerLimit"></param>
    public Operator(Player player, int permissionLevel = 4, bool bypassesPlayerLimit = true)
    {
        Player = player;
        PermissionLevel = permissionLevel;
        BypassesPlayerLimit = bypassesPlayerLimit;
    }
}