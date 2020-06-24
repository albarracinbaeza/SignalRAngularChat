using SignalRAngularChat.DTO;
using System.Collections.Generic;

namespace SignalRAngularChat.Application
{
    public interface IChatApplicationService
    {
        RoomDto GetRoomByRoomId(int roomId);
        RoomDto GetRoomByRoomName(string roomName);
        RoomUserDto GetRoomUserByRoomIdAndUserId(int roomId, int userId);
        UserDto GetUserByUserName(string userName);
        ChatDto InsertChat(ChatDto chat);
        RoomDto InsertRoom(RoomDto room);
        RoomUserDto InsertRoomUser(RoomUserDto roomUser);
        UserDto InsertUser(UserDto user);
        List<ChatDto> ListChatByRoomId(int roomId);
        List<RoomDto> ListRooms();
        List<UserDto> ListUsersByRoomId(int roomId);
        List<UserDto> ListUsersByRoomIdAndStatus(int roomId, string status);
        List<UserDto> ListUsersByRoomIdAndUserIdAndStatus(int roomId, int userId, string status);
        RoomUserDto UpdateRoomUser(RoomUserDto roomUser);
    }
}