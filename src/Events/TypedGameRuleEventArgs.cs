using MSMPSharp.Models.Game;

namespace MSMPSharp.Events;

public class TypedGameRuleEventArgs(TypedGameRule gameRule) : EventArgs
{
    public TypedGameRule GameRule { get; } = gameRule;
}
