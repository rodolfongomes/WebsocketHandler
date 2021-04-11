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
    }
}