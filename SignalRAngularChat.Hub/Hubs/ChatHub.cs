using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRAngularChat.Hub.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using SignalRAngularChat.DTO;

    public class ChatHub:Hub
    {
        public ChatHub()
        {

        }
        public async Task ListRoomsServer()
        {
            await Clients.All.SendAsync("ListRoomsClient");
        }

        public async Task SendChatServer(ChatDto chat)
        {
            await Clients.All.SendAsync("ReciveChatClient", chat);
        }

        public async Task ListUsersByRoomIdServer(int roomId)
        {
            await Clients.All.SendAsync("ListUsersByRoomIdClient", roomId);
        }
    }
}
