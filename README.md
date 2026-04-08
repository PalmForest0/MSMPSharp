# MSMPSharp

A C# client library for interacting with Minecraft servers via the **MSMP (Minecraft Server Management Protocol)** WebSocket API.
MSMPSharp provides a clean, asynchronous interface for managing players, bans, operators, game rules, server settings, and more over JSON-RPC.
For more info on the **MSMP** see: https://minecraft.wiki/w/Minecraft_Server_Management_Protocol

---

## Features

- **WebSocket-based** connection to a Minecraft server's MSMP endpoint
- **JSON-RPC** request/response and notification handling
- **Modular API** separates each area of server management into its own module
- **Async/await** pattern present in client and all logic

---

## Installation

Clone the repository and reference the `src/MSMPSharp.csproj` project in your solution:

```bash
git clone https://github.com/PalmForest0/MSMPSharp.git
```

Add a project reference in your `.csproj`:

```xml
<ProjectReference Include="../MSMPSharp/src/MSMPSharp.csproj" />
```

> [!NOTE]
> NuGet package is not yet available (see the TODO list below).

---

## Quick Start

```csharp
using MSMPSharp.Core;
using MSMPSharp.Data.Game;
using MSMPSharp.Data.Server;

await using var client = new MsmpClient("localhost", 25585, "KEY");

client.OnConnected += (s, e) => Console.WriteLine("Connected!");
client.OnDisconnected += (s, e) => Console.WriteLine("Disconnected.");

await client.ConnectAsync();

// List online players
var players = await client.Players.GetAsync();

Console.WriteLine($"Online Players ({players.Length}):");
foreach (var p in players)
{
    Console.WriteLine($"\t- {p.Name} ({p.Id})");
}

// Send a message to the server
await client.Server.SendSystemMessageAsync(new SystemMessage(players, new Message("Hello from MSMPSharp"), overlay: true));

// Ban a player
Player? player = players.FirstOrDefault(p => p.Name == "PlayerName", null);
if(player is not null)
{
    var ban = new UserBan(player, expires: DateTime.Now.AddHours(24), reason: "Breaking the rules", source: "MSMPSharp");
    await client.Bans.AddAsync([ban]);
}
```

---

## Modules

| Module | Description |
|---|---|
| `Players` | List and manage online players |
| `Allowlist` | Add, remove, and query the server allowlist |
| `Bans` | Manage player bans |
| `IpBans` | Manage IP-based bans |
| `Operators` | Manage server operators (ops) |
| `Server` | System messages, server state, saving and stopping |
| `GameRules` | Get and set game rules (typed and untyped) |
| `ServerSettings` | Read and modify server settings |

---

## TODO / Potential Improvements

- [ ] **NuGet package** - Publish to NuGet so the library can be installed without cloning the repo
- [ ] **CancellationToken** - Use the cancellation token throughout client logic instead of  `CancellationToken.None`
- [ ] **Reconnection logic** - Add automatic reconnection with exponential backoff
- [ ] **Timeout handling** - Add a configurable request timeout to prevent freezing
- [ ] **Replace Newtonsoft.Jsonn** - Reduces the reliance on external dependencies if possible
- [ ] **Structured logging** - Add `ILogger` / `Microsoft.Extensions.Logging` support so callers can control log output
- [ ] **Improve Usability** - Include more user-friendly methods in modules to make performing most actions more intuitive
- [ ] **Dependency injection** - Potentially register `MsmpClient` as a DI service to allow for cleaner usage
- [ ] **Strongly-typed notifications** — Replace Actions and `JsonRpcNotification` with typed events for notifications

---

## Contributing

Any pull requests and issues are welcome. This project is currently very small, so laying a foundation is the priority.
