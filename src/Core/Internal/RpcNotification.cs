using System.Text.Json;
using System.Text.Json.Nodes;
using System.Diagnostics.CodeAnalysis;

namespace MSMPSharp.Core.Internal;

internal sealed class RpcNotification
{
    public string Jsonrpc { get; } = "2.0";
    public required string Method { get; init; }
    public required JsonNode? Params { get; init; }

    /// <summary>
    /// Deserializes the notification params into <typeparamref name="T"/>.
    /// Throws if params are missing, don't fit T, or convert to null.
    /// </summary>
    public T GetParams<T>()
    {
        if (Params is null)
            throw new InvalidOperationException($"Notification '{Method}' has no params.");

        try
        {
            return Params.Deserialize<T>()
                ?? throw new InvalidOperationException($"Notification '{Method}' params cannot be converted to type {nameof(T)}.");
        }
        catch (JsonException) when (Params is JsonArray { Count: 1 } single)
        {
            // Params was a single-element array containing the actual param.
            return single[0].Deserialize<T>()
                ?? throw new InvalidOperationException($"Notification '{Method}' params cannot be converted to type {nameof(T)}.");
        }
    }

    /// <summary>
    /// Attempts to deserialize the notification params into <typeparamref name="T"/>.
    /// Never throws.
    /// </summary>
    public bool TryGetParams<T>([NotNullWhen(true)] out T result)
    {
        try
        {
            result = GetParams<T>();
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}