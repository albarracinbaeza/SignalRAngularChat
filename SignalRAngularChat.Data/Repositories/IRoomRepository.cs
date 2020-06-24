using SignalRAngularChat.DTO;
using System.Collections.Generic;

namespace SignalRAngularChat.Data.Repositories
{
    public interface IRoomRepository
    {
        RoomDto GetByRoomId(int roomId);
        RoomDto GetByRoomName(string roomName);
        RoomDto Insert(RoomDto room);
        List<RoomDto> ListRooms();
    }
}