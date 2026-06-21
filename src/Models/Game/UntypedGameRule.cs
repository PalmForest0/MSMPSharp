namespace MSMPSharp.Models.Game;

public sealed class UntypedGameRule
{
    public string Key { get; set; }
    public string Value { get; set; }

    public UntypedGameRule(string key, string value)
    {
        Key = key;
        Value = value;
    }

    /// <summary>
    /// Create untyped game rule from touple.
    /// </summary>
    /// <param name="pair">A touple containing a key string and value string.</param>
    public static implicit operator UntypedGameRule((string key, string value) pair) => new UntypedGameRule(pair.key, pair.value);

    /// <summary>
    /// Create untyped game rule from typed game rule.
    /// </summary>
    /// <param name="rule">Typed game rule.</param>
    public static implicit operator UntypedGameRule(TypedGameRule rule) => new UntypedGameRule(rule.Key, rule.Value);
}