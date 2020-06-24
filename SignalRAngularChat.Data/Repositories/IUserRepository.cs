using SignalRAngularChat.DTO;
using System.Collections.Generic;

namespace SignalRAngularChat.Data.Repositories
{
    public interface IUserRepository
    {
        UserDto GetByUserName(string userName);
        UserDto Insert(UserDto user);
        List<UserDto> ListUsersByRoomId(int roomId);
        List<UserDto> ListUsersByRoomIdAndStatus(int roomId, string status);
        List<UserDto> ListUsersByRoomIdAndUserIdAndStatus(int roomId, int userId, string status);
    }
}