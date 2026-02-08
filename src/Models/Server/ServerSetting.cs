namespace MSMPSharp.Models.Server;

public sealed class ServerSetting<T>
{
    /// <summary>
    /// Gets the current value.
    /// </summary>
    /// <returns>The current server setting value.</returns>
    public T Get() => value;

    /// <summary>
    /// Sets the value and returns the updated value.
    /// </summary>
    /// <param name="value">The new value to set.</param>
    /// <returns>The updated value after the set operation.</returns>
    public T Set(T value) => this.value = value;
}