using MSMPSharp.Models.Server;

namespace MSMPSharp.Events;

public sealed class BanEventArgs : EventArgs
{
    public UserBan UserBan { get; }

    internal BanEventArgs(UserBan userBan)
    {
        UserBan = userBan;
    }
}