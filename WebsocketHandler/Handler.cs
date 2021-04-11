using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebsocketHandler
{
    public abstract class Handler
    {
        protected ConnectionManager WebSocketsConnManager { get; set; }

        protected Handler(ConnectionManager webSocketConnectionManager)
        {
            WebSocketsConnManager = webSocketConnectionManager;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            WebSocketsConnManager.AddSocket(socket);
        }

        public virtual async Task OnConnected(string socketId, WebSocket socket)
        {
            WebSocketsConnManager.AddSocket(socketId, socket);
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await WebSocketsConnManager.RemoveSocket(WebSocketsConnManager.GetId(socket));
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                  offset: 0,
                                                                  count: message.Length),
                                   messageType: WebSocketMessageType.Text,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None);
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(WebSocketsConnManager.GetSocketById(socketId), message);
        }

        public async Task SendMessageToAllAsync(string message)
        {
            foreach (var pair in WebSocketsConnManager.GetAll())
            {
                if (pair.Value.State == WebSocketState.Open)
                    await SendMessageAsync(pair.Value, message);
            }
        }

        public abstract Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}