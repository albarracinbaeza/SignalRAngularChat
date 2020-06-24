using SignalRAngularChat.Data.Contexts;
using SignalRAngularChat.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SignalRAngularChat.Data.Entities;

namespace SignalRAngularChat.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly SignalRAngularChatContext _context;
        public ChatRepository(SignalRAngularChatContext context)
        {
            _context = context;
        }
        public List<ChatDto> ListByRoomId(int roomId)
        {
            var query = from c in _context.Chats
                        where c.RoomId == roomId
                        select new ChatDto
                        {
                            ChatId = c.ChatId,
                            Date = c.Date,
                            Message = c.Message,
                            RoomId = c.RoomId,
                            UserId = c.UserId,
                            Type = c.Type
                        };
            List<ChatDto> chats = query.ToList();

            return chats;
        }

        public ChatDto Insert(ChatDto chat)
        {
            Chat newChat = new Chat
            {
                Date = chat.Date,
                Message = chat.Message,
                RoomId = chat.RoomId,
                UserId = chat.UserId,
                Type = chat.Type
            };

            _context.Chats.Add(newChat);
            _context.SaveChanges();

            chat.ChatId = newChat.ChatId;
            return chat;
        }
    }
}
