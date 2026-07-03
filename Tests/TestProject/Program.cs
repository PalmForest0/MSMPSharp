using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;
using MSMPSharp.Modules;
using System.Text.Json.Nodes;

// Do not warn about unused methods
#pragma warning disable CS8321

// Create the client
await using var client = MsmpClient.CreateBuilder()
    .WithHost("localhost", 25585)
    .WithSecret("n09TPqHgJtqtUvCrhebO0DxcJtaW8Io9hyjbEw1y")
    .Build();

// Subscribe to events with console log statements
client.Connected += (sender, e) => Console.WriteLine($"Connected to {e.ServerUri}.");
client.Disconnected += (sender, e) => Console.WriteLine($"Disconnected from {e.ServerUri}.");
client.Players.PlayerJoined += (sender, e) => Console.WriteLine($"{e.Player.Name} ({e.Player.Id}) joined the game.");
client.Players.PlayerLeft += (sender, e) => Console.WriteLine($"{e.Player.Name} ({e.Player.Id}) left the game.");

// Connect to the server
await client.ConnectAsync();

await SaveSchema(client);
//Player player = await GetTestPlayer(client);
//await TestSystemMessage(client, player);

// Disconnect from the server
await client.DisconnectAsync();


static async Task SaveSchema(MsmpClient client)
{
    JsonObject schema = await client.GetSchemaAsync();
    await File.WriteAllTextAsync("schema.json", schema.ToJsonString(options: new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
}

static async Task<Player> GetTestPlayer(MsmpClient client)
{
    Player? player = null;

    do
    {
        Console.Write("Enter test player name: ");
        string? name = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(name))
            player = await client.Players.GetFirstAsync(player => player.Name == name);

        if(player is null)
            Console.WriteLine("Player not found, try again.");
    }
    while (player is null);

    return player;
}

static async Task TestSystemMessage(MsmpClient client, Player player)
{
    await client.Server.SendSystemMessageAsync(SystemMessage.InChat(Message.FromTranslatable("advancements.adventure.spyglass_at_parrot.title"), player));
}

static async Task TestIpBan(MsmpClient client, Player player)
{
    await client.IpBans.ClearAsync();
    await client.IpBans.AddAsync(IncomingIpBan.ToPlayer(player, reason: "Test", expires: DateTime.Now.AddSeconds(10), source: "MSMPSharp"));
}

static async Task TestKick(MsmpClient client, Player player)
{
    await client.Players.KickAsync(new KickPlayer(player, Message.FromTranslatable("advancements.adventure.spyglass_at_parrot.title")));
}

static async Task TestMotd(MsmpClient client, int repeatCount)
{
    for (int i = 0; i < repeatCount; i++)
    {
        await Task.Delay(4000);
        await client.ServerSettings.Motd.SetAsync("is it working?");
        await Task.Delay(4000);
        await client.ServerSettings.Motd.SetAsync("woah it works");
    }
}