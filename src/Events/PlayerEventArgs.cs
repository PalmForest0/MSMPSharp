using MSMPSharp.Models.Game;

namespace MSMPSharp.Events;

public class PlayerEventArgs : EventArgs
{
    public Player Player { get; }

    public PlayerEventArgs(Player player) => Player = player;
}