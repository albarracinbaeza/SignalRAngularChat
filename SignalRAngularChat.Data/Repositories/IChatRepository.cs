using SignalRAngularChat.DTO;
using System.Collections.Generic;

namespace SignalRAngularChat.Data.Repositories
{
    public interface IChatRepository
    {
        ChatDto Insert(ChatDto chat);
        List<ChatDto> ListByRoomId(int roomId);
    }
}