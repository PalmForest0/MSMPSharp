namespace MSMPSharp.Models.Server;

public class Message
{
    public string? Literal { get; set; }
    public string? Translatable { get; set; }
    public string[]? TranslatableParams { get; set; }

    /// <summary>
    /// Creates a normal message using a string literal.
    /// </summary>
    /// <param name="literal">String literal to create the message with.</param>
    public Message(string literal)
    {
        Literal = literal;
    }

    /// <summary>
    /// Creates a translatable message using a translation key and array of params.
    /// </summary>
    /// <param name="translationKey">The key of the translatable string for this message.</param>
    /// <param name="translationParams">The params for the translatable string.</param>
    public Message(string translationKey, string[] translationParams)
    {
        Translatable = translationKey;
        TranslatableParams = translationParams;
    }
}