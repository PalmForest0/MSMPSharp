using MSMPSharp.Models.Game;

namespace MSMPSharp.Events;

public class PlayerEventArgs(Player player) : EventArgs
{
    public Player Player { get; } = player;
}