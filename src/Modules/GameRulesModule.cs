using MSMPSharp.Core;
using MSMPSharp.Events;
using MSMPSharp.Models.Game;

namespace MSMPSharp.Modules;

public sealed class GameRulesModule : ModuleBase
{
    internal GameRulesModule(MsmpClient client) : base(client)
    {
        client.SetNotificationHandler("minecraft:notification/gamerules/updated", notif =>
        {
            if (notif.TryGetParams<TypedGameRule[]>(out var rules) && rules is not null && rules.Length > 0)
                Updated?.Invoke(this, new TypedGameRuleEventArgs(rules[0]));
        });
    }

    /// <summary>
    /// An event that is invoked when a game rule was changed.
    /// </summary>
    public event EventHandler<TypedGameRuleEventArgs>? Updated;

    /// <summary>
    /// Get the available game rule keys and their current values.
    /// </summary>
    /// <returns>An array of available game rules.</returns>
    public async Task<TypedGameRule[]> GetAsync() => await client.SendAsync<TypedGameRule[]>("minecraft:gamerules");

    /// <summary>
    /// Updates the value of a game rule.
    /// </summary>
    /// <returns>The updated game rule.</returns>
    public async Task<TypedGameRule> UpdateAsync(UntypedGameRule gamerule) => await client.SendAsync<TypedGameRule>("minecraft:gamerules/update", [gamerule]);
}