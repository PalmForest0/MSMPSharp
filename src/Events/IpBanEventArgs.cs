using MSMPSharp.Models.Server;

namespace MSMPSharp.Events;

public sealed class IpBanEventArgs : EventArgs
{
    public IpBan IpBan { get; }

    internal IpBanEventArgs(IpBan ipBan)
    {
        IpBan = ipBan;
    }
}