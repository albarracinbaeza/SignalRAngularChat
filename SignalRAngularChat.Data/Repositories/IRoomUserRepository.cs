using SignalRAngularChat.DTO;

namespace SignalRAngularChat.Data.Repositories
{
    public interface IRoomUserRepository
    {
        RoomUserDto GetByRoomIdAndUserId(int roomId, int userId);
        RoomUserDto Insert(RoomUserDto roomUser);
        RoomUserDto Update(RoomUserDto roomUser);
    }
}