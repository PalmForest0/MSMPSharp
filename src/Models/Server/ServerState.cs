using MSMPSharp.Models.Game;
using System.Text.Json.Serialization;

namespace MSMPSharp.Models.Server;

public sealed class ServerState
{
    public Player[] Players { get; set; }
    public Version Version { get; set; }
    public bool Started { get; set; }

    [JsonConstructor]
    private ServerState(Player[] players, Version version, bool started)
    {
        Players = players;
        Version = version;
        Started = started;
    }
}