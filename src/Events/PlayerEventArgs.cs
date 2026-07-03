using MSMPSharp.Models.Game;

namespace MSMPSharp.Events;

public sealed class PlayerEventArgs : EventArgs
{
    public Player Player { get; }

    internal PlayerEventArgs(Player player)
    {
        Player = player;
    }
}