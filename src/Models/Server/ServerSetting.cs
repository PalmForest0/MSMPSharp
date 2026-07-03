using MSMPSharp.Core;

namespace MSMPSharp.Models.Server;

public sealed class ServerSetting<T>
{
    private readonly MsmpClient _client;
    private readonly string _name;

    internal ServerSetting(MsmpClient client, string name)
    {
        _client = client;
        _name = name;
    }

    /// <summary>
    /// Gets the current value.
    /// </summary>
    /// <returns>The current server setting value.</returns>
    public async Task<T> GetAsync() => await _client.SendAsync<T>($"minecraft:serversettings/{_name}");

    /// <summary>
    /// Sets the value and returns the updated value.
    /// </summary>
    /// <param name="value">The new value to set.</param>
    /// <returns>The updated value after the set operation.</returns>
    public async Task<T> SetAsync(T value) => await _client.SendAsync<T>($"minecraft:serversettings/{_name}/set", [value!]);
}