using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;
using Newtonsoft.Json.Linq;

await using var client = MsmpClient.CreateBuilder()
    .WithHost("localhost", 25585)
    .WithSecret("n09TPqHgJtqtUvCrhebO0DxcJtaW8Io9hyjbEw1y")
    .Build();

client.OnConnected += (_, _) => Console.WriteLine("Connected.");
client.OnDisconnected += (_, _) => Console.WriteLine("Disconnected.");
client.Players.PlayerJoined += player => Console.WriteLine($"{player.Name} ({player.Id}) joined the game.");
client.Players.PlayerLeft += player => Console.WriteLine($"{player.Name} ({player.Id}) left the game.");

await client.ConnectAsync();
await client.IpBans.ClearAsync();

Console.Write("Enter player name: ");
string? name = Console.ReadLine();
Player? testPlayer = null;

if(!string.IsNullOrWhiteSpace(name))
    testPlayer = await client.Players.GetFirstAsync(player => player.Name == name);

await IpBanTest();

async Task SaveSchema()
{
    JObject schema = await client.GetSchemaAsync();
    //Console.WriteLine(schema.ToString(Newtonsoft.Json.Formatting.Indented));
    await File.WriteAllTextAsync("schema.json", schema.ToString(Newtonsoft.Json.Formatting.Indented));
}

async Task IpBanTest()
{
    await client.IpBans.ClearAsync();
    //var bans = await client.Bans.GetAsync();

    await client.IpBans.AddAsync([
        new IncomingIpBan(testPlayer, DateTime.Now.AddSeconds(10))]);
}

async Task TestKick()
{
    await client.Players.KickAsync(new KickPlayer(
        testPlayer,
        new Message("advancements.adventure.spyglass_at_parrot.title", [])));
}

async Task TestMotd(int repeatCount)
{
    for (int i = 0; i < repeatCount; i++)
    {
        await Task.Delay(4000);
        await client.ServerSettings.Motd.SetAsync("is it working?");
        await Task.Delay(4000);
        await client.ServerSettings.Motd.SetAsync("woah it works");
    }
}