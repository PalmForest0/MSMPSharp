using System.Net.WebSockets;
using System.Text;

namespace MSMPSharp.Extensions;

internal static class WebSocketExtensions
{
    extension(ClientWebSocket socket)
    {
        public async Task<string> ReceiveInChunksAsync(CancellationToken cancellationToken)
        {
            var buffer = new byte[4096];
            using var ms = new MemoryStream();

            WebSocketReceiveResult result;

            do
            {
                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);

                if (result.MessageType == WebSocketMessageType.Close)
                    throw new WebSocketException("WebSocket closed unexpectedly.");

                await ms.WriteAsync(buffer, 0, result.Count);
            }
            while (!result.EndOfMessage);

            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
