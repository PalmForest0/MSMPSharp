using MSMPSharp.Models.Server;

namespace MSMPSharp.Events;

public class BanEventArgs(UserBan userBan) : EventArgs
{
    public UserBan UserBan { get; } = userBan;
}