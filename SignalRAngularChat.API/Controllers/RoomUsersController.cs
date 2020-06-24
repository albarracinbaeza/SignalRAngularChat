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
    public class RoomUsersController : ControllerBase
    {
        private readonly IChatApplicationService _chatApplicationService;
        private readonly HubConnection _chatHubConnection;
        public RoomUsersController(IChatApplicationService chatApplicationService)
        {
            _chatApplicationService = chatApplicationService;
            _chatHubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44355/ChatHub")
                .Build();
        }

        [HttpGet()]
        public RoomUserDto Get(
            [FromQuery(Name = "roomId")] int roomId,
            [FromQuery(Name = "userId")] int userId)
        {
            return _chatApplicationService.GetRoomUserByRoomIdAndUserId(roomId,userId);
        }

        [HttpPost]
        public async Task<RoomUserDto> PostAsync([FromBody] RoomUserDto roomUser)
        {
            roomUser = _chatApplicationService.InsertRoomUser(roomUser);

            await _chatHubConnection.StartAsync();
            await _chatHubConnection.InvokeAsync("ListUsersByRoomIdServer",roomUser.RoomId);

            return roomUser;
        }
        [HttpPut]
        public async Task<RoomUserDto> PutAsync([FromBody] RoomUserDto roomUser)
        {
            roomUser = _chatApplicationService.UpdateRoomUser(roomUser);

            await _chatHubConnection.StartAsync();
            await _chatHubConnection.InvokeAsync("ListUsersByRoomIdServer", roomUser.RoomId);

            return roomUser;
        }
    }
}
