using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SignalRAngularChat.Application;
using SignalRAngularChat.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SignalRAngularChat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IChatApplicationService _chatApplicationService;
        public UsersController(IChatApplicationService chatApplicationService)
        {
            _chatApplicationService = chatApplicationService;
        }
 
        [HttpGet("{userName}")]
        public UserDto Get(string userName)
        {
            return _chatApplicationService.GetUserByUserName(userName);
        }

        [HttpGet()]
        public List<UserDto> List(
            [FromQuery(Name ="roomId")] int roomId,
            [FromQuery(Name = "userId")] int userId,
            [FromQuery(Name = "status")] string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return _chatApplicationService.ListUsersByRoomId(roomId);
            }
            else
            {
                return _chatApplicationService.ListUsersByRoomIdAndStatus(roomId, status);
            }
        }

        //[HttpGet()]
        //public List<UserDto> ListByRoomId(
        //    [FromQuery(Name = "roomId")] int roomId)
        //{
        //    return _chatApplicationService.ListUsersByRoomId(roomId);
        //}

        // POST api/<UsersController>
        [HttpPost]
        public UserDto Post([FromBody] UserDto user)
        {
            return _chatApplicationService.InsertUser(user);
        }       
    }
}
