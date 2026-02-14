using MSMPSharp.Data.Game;

namespace MSMPSharp.Data.Server;

public class ServerState
{
    public Player[] Players { get; set; }
    public Version Version { get; set; }
    public bool Started { get; set; }
}