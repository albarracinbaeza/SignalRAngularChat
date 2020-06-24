using SignalRAngularChat.Data.Contexts;
using SignalRAngularChat.Data.Entities;
using SignalRAngularChat.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SignalRAngularChat.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly SignalRAngularChatContext _context;
        public RoomRepository(SignalRAngularChatContext context)
        {
            _context = context;
        }
        public RoomDto GetByRoomName(string roomName)
        {
            var query = from r in _context.Rooms
                        where r.RoomName == roomName
                        select new RoomDto
                        {
                            RoomId = r.RoomId,
                            RoomName = r.RoomName
                        };
            RoomDto room = query.FirstOrDefault();

            return room;
        }
        public RoomDto GetByRoomId(int roomId)
        {
            var query = from r in _context.Rooms
                        where r.RoomId == roomId
                        select new RoomDto
                        {
                            RoomId = r.RoomId,
                            RoomName = r.RoomName
                        };
            RoomDto room = query.FirstOrDefault();

            return room;
        }

        public List<RoomDto> ListRooms()
        {
            var query = from r in _context.Rooms
                        select new RoomDto
                        {
                            RoomId = r.RoomId,
                            RoomName = r.RoomName
                        };
            List<RoomDto> rooms = query.ToList();

            return rooms;
        }
        public RoomDto Insert(RoomDto room)
        {
            Room newRoom = new Room { RoomName = room.RoomName };
            _context.Rooms.Add(newRoom);
            _context.SaveChanges();

            room.RoomId = newRoom.RoomId;
            return room;
        }
    }
}
