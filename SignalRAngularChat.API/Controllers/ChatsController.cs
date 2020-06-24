using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using SignalRAngularChat.Application;
using SignalRAngularChat.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalRAngularChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        private readonly IChatApplicationService _chatApplicationService;
        private readonly HubConnection _chatHubConnection;
        public ChatsController(IChatApplicationService chatApplicationService)
        {
            _chatApplicationService = chatApplicationService;
            _chatHubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44355/ChatHub")
                .Build();
        }

        [HttpGet()]
        public List<ChatDto> List([FromQuery(Name = "roomId")] int roomId)
        {
            return _chatApplicationService.ListChatByRoomId(roomId);
        }

        [HttpPost]
        public ChatDto Post([FromBody] ChatDto chat)
        {
            chat = _chatApplicationService.InsertChat(chat);
            return chat;
        }       
    }
}
