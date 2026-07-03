using MSMPSharp.Core;
using MSMPSharp.Events;
using MSMPSharp.Models.Game;

namespace MSMPSharp.Modules;

public sealed class GameRulesModule : ModuleBase
{
    internal GameRulesModule(MsmpClient client) : base(client) { }

    internal override void RegisterNotificationHandlers()
    {
        _client.SetNotificationHandler("minecraft:notification/gamerules/updated", notif =>
        {
            if (notif.TryGetParams<TypedGameRule>(out var rule))
                Updated?.Invoke(this, new GameRuleEventArgs(rule));
        });
    }

    /// <summary>
    /// An event that is invoked when a game rule was changed.
    /// </summary>
    public event EventHandler<GameRuleEventArgs>? Updated;

    /// <summary>
    /// Get the available game rule keys and their current values.
    /// </summary>
    /// <returns>An array of available game rules.</returns>
    public async Task<TypedGameRule[]> GetAsync() => await _client.SendAsync<TypedGameRule[]>("minecraft:gamerules");

    /// <summary>
    /// Updates the value of a game rule.
    /// </summary>
    /// <param name="gamerule">The untyped game rule to update, created using a string key and value.</param>
    /// <returns>The updated game rule.</returns>
    public async Task<TypedGameRule> UpdateAsync(UntypedGameRule gamerule) => await _client.SendAsync<TypedGameRule>("minecraft:gamerules/update", [gamerule]);

    /// <summary>
    /// Updates the value of a game rule.
    /// </summary>
    /// <param name="key">The key of the game rule to update.</param>
    /// <param name="value">The new value for the game rule.</param>
    /// <returns>The updated game rule as a <see cref="TypedGameRule"/>.</returns>
    public async Task<TypedGameRule> UpdateAsync(string key, string value) => await _client.SendAsync<TypedGameRule>("minecraft:gamerules/update", [new UntypedGameRule(key, value)]);
}