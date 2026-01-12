using MSMPSharp.Models.Game;

namespace MSMPSharp.Models.Server;

public class ServerState
{
    public Player[] Players { get; set; }
    public Version Version { get; set; }
    public bool Started { get; set; }
}