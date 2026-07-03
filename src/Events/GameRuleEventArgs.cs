using MSMPSharp.Models.Game;

namespace MSMPSharp.Events;

public sealed class GameRuleEventArgs : EventArgs
{
    public TypedGameRule GameRule { get; }

    internal GameRuleEventArgs(TypedGameRule gameRule)
    {
        GameRule = gameRule;
    }
}
