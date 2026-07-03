using MSMPSharp.Models.Server;

namespace MSMPSharp.Events;

public sealed class ServerStateEventArgs : EventArgs
{
    public ServerState State { get; }

    internal ServerStateEventArgs(ServerState state)
    {
        State = state;
    }
}
