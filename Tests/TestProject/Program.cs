using MSMPSharp.Core;

await using var client = new MsmpClient("localhost", 25585, "n09TPqHgJtqtUvCrhebO0DxcJtaW8Io9hyjbEw1y");
await client.ConnectAsync();

while (true)
{
    await Task.Delay(4000);
    await client.ServerSettings.Motd.SetAsync("is it working?");
    await Task.Delay(4000);
    await client.ServerSettings.Motd.SetAsync("woah it works");
}