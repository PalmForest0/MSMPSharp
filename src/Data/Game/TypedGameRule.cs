namespace MSMPSharp.Data.Game;

public sealed class TypedGameRule
{
    public string Type { get; set; } = "";
    public string Value { get; set; } = "";
    public string Key { get; set; } = "";

    // A typed game rule should never be created by users
    internal TypedGameRule() { }

    /// <summary>
    /// Attempts to get the value of this typed game rule as an <see langword="int"/>.
    /// </summary>
    /// <param name="value">The successfully parsed value.</param>
    /// <returns>True if the value was parsed successfully.</returns>
    public bool TryGetInt(out int value) => int.TryParse(Value, out value);

    /// <summary>
    /// Attempts to get the value of this typed game rule as a <see langword="bool"/>.
    /// </summary>
    /// <param name="value">The successfully parsed value.</param>
    /// <returns>True if the value was parsed successfully.</returns>
    public bool TryGetBool(out bool value) => bool.TryParse(Value, out value);
}