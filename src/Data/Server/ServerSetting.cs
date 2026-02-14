using MSMPSharp.Core;

namespace MSMPSharp.Data.Server;

public sealed class ServerSetting<T>
{
    private readonly MsmpClient client;
    private readonly string settingName;

    internal ServerSetting(MsmpClient client, string settingName)
    {
        this.client = client;
        this.settingName = settingName;
    }

    /// <summary>
    /// Gets the current value.
    /// </summary>
    /// <returns>The current server setting value.</returns>
    public async Task<T> GetAsync() => await client.CallMethodAsync<T>($"minecraft:serversettings/{settingName}");

    /// <summary>
    /// Sets the value and returns the updated value.
    /// </summary>
    /// <param name="value">The new value to set.</param>
    /// <returns>The updated value after the set operation.</returns>
    public async Task<T> SetAsync(T value) => await client.CallMethodAsync<T>($"minecraft:serversettings/{settingName}/set", [value]);
}