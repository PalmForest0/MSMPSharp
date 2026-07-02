using MSMPSharp.Models.Server;

namespace MSMPSharp.Events;

public class ServerStateEventArgs(ServerState state) : EventArgs
{
    public ServerState State { get; } = state;
}
