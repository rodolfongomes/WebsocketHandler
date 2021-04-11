using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebsocketHandler.Demo.Api.Handlers;
using WebsocketHandler.Demo.Api.Result;
using System.Text.Json.Serialization;

namespace WebsocketHandler.Demo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private PushHandler _notifications { get; set; }

        public MessageController(PushHandler notificationsMessageHandler)
        {
            _notifications = notificationsMessageHandler;
        }

        [HttpGet("sendmessage")]
        public async Task SendMessage([FromBody] ReturnResult message)
        {
            var _ret = JsonConvert.SerializeObject(message);

            await _notifications.SendMessageAsync(_notifications.SocketId, $"sockedid:{_notifications.SocketId}, data:{_ret }");
        }

        //[HttpGet("receivemessage")]
        //public async Task<string> ReceiveMessage()
        //{
        //    var client = new ClientWebSocket();

        //    await client.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);

        //    var buffer = new byte[1024 * 4];

        //    var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        //    if (result.MessageType == WebSocketMessageType.Text)
        //        return await Task.FromResult(Encoding.UTF8.GetString(buffer, 0, result.Count));
        //    else if (result.MessageType == WebSocketMessageType.Close)
        //    {
        //        await client.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);

        //        return await Task.FromResult("Connection Closed");
        //    }
        //    else { return await Task.FromResult("Nothing"); }
        //}
    }
}