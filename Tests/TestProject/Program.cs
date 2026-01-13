using MSMPSharp.Core;
using MSMPSharp.Models.Game;
using MSMPSharp.Models.Server;
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

await client.Server.SendSystemMessageAsync(new SystemMessage()
{
    Message = new Message()
    {
        Literal = "bro what"
    },
    Overlay = true,
    ReceivingPlayers = await client.Players.GetAsync()
});

//string input = "";

//do
//{
//    Console.Write("---------------------------------\nEnter a method and its params: ");
//    input = Console.ReadLine() ?? "rpc.discover";

//    try
//    {
//        var result = await client.CallMethodAsync<JToken>(input);
//        Console.WriteLine(result.ToString(Formatting.Indented));
//    }
//    catch(Exception e)
//    {
//        Console.WriteLine($"Invalid method request:\n{e.Message}"); 
//    }
//}
//while (!string.IsNullOrWhiteSpace(input) && input.ToLowerInvariant() != "stop");


await client.DisconnectAsync();