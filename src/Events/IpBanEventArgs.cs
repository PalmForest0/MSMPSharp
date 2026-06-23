using MSMPSharp.Models.Server;

namespace MSMPSharp.Events;

public class IpBanEventArgs(IpBan ipBan) : EventArgs
{
    public IpBan IpBan { get; } = ipBan;
}