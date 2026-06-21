using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

public class Operator
{
    public Player Player { get; set; }
    public int PermissionLevel { get; set; }
    public bool BypassesPlayerLimit { get; set; }

    /// <summary>
    /// Creates the data for a server operator using a player.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="permissionLevel"></param>
    /// <param name="bypassesPlayerLimit"></param>
    public Operator(Player player, int permissionLevel = 4, bool bypassesPlayerLimit = true)
    {
        Player = player;
    }
}