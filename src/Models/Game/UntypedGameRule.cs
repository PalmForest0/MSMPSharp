namespace MSMPSharp.Models.Game;

public sealed class UntypedGameRule(string key, string value)
{
    public string Key { get; } = key;
    public string Value { get; } = value;

    /// <summary>
    /// Create untyped game rule from typed game rule.
    /// </summary>
    /// <param name="rule">Typed game rule.</param>
    public static implicit operator UntypedGameRule(TypedGameRule rule) => new UntypedGameRule(rule.Key, rule.Value);
}