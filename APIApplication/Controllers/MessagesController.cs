using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _hubContext;

        public MessagesController(IHubContext<MessageHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("broadcast")]
        public async Task<IActionResult> Broadcast([FromBody] MessageDto message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.User, message.Message);
            return Ok();
        }

        [HttpPost("sendToUser")]
        public async Task<IActionResult> SendToUser([FromBody] UserMessageDto message)
        {
            var connectionId = MessageHub.GetConnectionId(message.TargetUser);
            if (connectionId != null)
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", message.User, message.Message);
            }
            return Ok();
        }

        [HttpGet("onlineUsers")]
        public IActionResult GetOnlineUsers()
        {
            var onlineUsers = MessageHub.GetOnlineUsers();
            return Ok(onlineUsers);
        }
    }

    public class MessageDto
    {
        public string User { get; set; }
        public string Message { get; set; }
    }

    public class UserMessageDto
    {
        public string User { get; set; }
        public string Message { get; set; }
        public string TargetUser { get; set; }
    }
}
