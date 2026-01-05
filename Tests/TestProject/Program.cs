using MSMPSharp.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

var client = new MsmpClient("localhost", 25585);
await client.ConnectAsync();

var schema = await client.CallMethodAsync<JObject>("rpc.disover");

Console.WriteLine(schema.ToString(Formatting.Indented));
await File.WriteAllTextAsync("rpc_schema.json", schema.ToString(Formatting.Indented));