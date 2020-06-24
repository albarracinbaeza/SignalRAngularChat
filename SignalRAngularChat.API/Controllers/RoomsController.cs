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
    public class RoomsController : ControllerBase
    {
        private readonly IChatApplicationService _chatApplicationService;
        private readonly HubConnection _chatHubConnection;
        public RoomsController(IChatApplicationService chatApplicationService)
        {
            _chatApplicationService = chatApplicationService;
            _chatHubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44355/ChatHub")
                .Build();
        }


        [HttpGet("{roomId}")]
        public RoomDto Get(int roomId)
        {
            return _chatApplicationService.GetRoomByRoomId(roomId);
        }

        [HttpGet()]
        public dynamic GetByRoomId([FromQuery(Name = "roomName")]string roomName)
        {
            if (string.IsNullOrEmpty(roomName))
            {
                return _chatApplicationService.ListRooms();
            }
            else
            {
                return _chatApplicationService.GetRoomByRoomName(roomName);
            }            
        }

        //[HttpGet()]
        //public List<RoomDto> List()
        //{
        //    return _chatApplicationService.ListRooms();
        //}

        [HttpPost]
        public async Task<RoomDto> PostAsync([FromBody] RoomDto room)
        {
            room = _chatApplicationService.InsertRoom(room);
            await _chatHubConnection.StartAsync();
            await _chatHubConnection.InvokeAsync("ListRoomsServer");

            return room;
        }       
    }
}
