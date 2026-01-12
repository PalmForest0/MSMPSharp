using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var client = new MsmpClient("localhost", 25585, "n09TPqHgJtqtUvCrhebO0DxcJtaW8Io9hyjbEw1y");
await client.ConnectAsync();

//var schema = await client.CallMethodAsync<JObject>("rpc.discover");

//Console.WriteLine(schema.ToString(Formatting.Indented));
//await File.WriteAllTextAsync("rpc_schema.json", schema.ToString(Formatting.Indented));

//Player[] players = await client.CallMethodAsync<Player[]>("players");

//foreach(var player in players)
//    Console.WriteLine($"Name: {player.Name}, Id: {player.Id}");

string? input = "";

do
{
    Console.WriteLine("Enter a method and its params: ");
    input = Console.ReadLine();

    var result = await client.CallMethodAsync<JObject>(input);
    Console.WriteLine(result.ToString(Formatting.Indented));
}
while (!string.IsNullOrWhiteSpace(input) && input.ToLowerInvariant() != "stop");


await client.DisconnectAsync();

