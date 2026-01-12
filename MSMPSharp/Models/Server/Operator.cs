using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

public class Operator
{
    public Player Player { get; set; }
    public int PermissionLevel { get; set; }
    public bool BypassesPlayerLimit { get; set; }
}