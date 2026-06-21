namespace MSMPSharp.Models.Server;

public sealed class Message
{
    public string? Literal { get; }
    public string? Translatable { get; }
    public string[]? TranslatableParams { get; }

    private Message(string? literal, string? translatable, string[]? translatableParams)
    {
        Literal = literal;
        Translatable = translatable;
        TranslatableParams = translatableParams;
    }

    /// <summary>
    /// Creates a normal string message using a string literal.
    /// </summary>
    /// <param name="literal">String literal to create the message with.</param>
    public static Message FromLiteral(string literal) => new Message(literal, null, null);

    /// <summary>
    /// Creates a translatable message using a translation key and array of params.
    /// </summary>
    /// <param name="key">The key of the translatable string for this message.</param>
    /// <param name="parameters">The params for the translatable string.</param>
    public static Message FromTranslatable(string key, params string[] parameters) => new Message(null, key, parameters);

    /// <summary>
    /// Create a a message using a string literal implicitly.
    /// </summary>
    /// <param name="literal">String literal to create a message from.</param>
    public static implicit operator Message(string literal) => Message.FromLiteral(literal);

    public override string ToString() => Literal ?? Translatable ?? string.Empty;
}