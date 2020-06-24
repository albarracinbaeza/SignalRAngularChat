using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRAngularChat.DTO
{
    public class ChatDto
    {
        public int ChatId { get; set; }
        public DateTime? Date { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public string Type { get; set; }
    }
}
