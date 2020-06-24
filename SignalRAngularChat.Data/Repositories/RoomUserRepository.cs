using SignalRAngularChat.Data.Contexts;
using SignalRAngularChat.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SignalRAngularChat.Data.Entities;

namespace SignalRAngularChat.Data.Repositories
{
    public class RoomUserRepository : IRoomUserRepository
    {
        private readonly SignalRAngularChatContext _context;
        public RoomUserRepository(SignalRAngularChatContext context)
        {
            _context = context;
        }
        public RoomUserDto GetByRoomIdAndUserId(int roomId, int userId)
        {
            var query = from ru in _context.RoomUsers
                        where ru.RoomId == roomId && ru.UserId == userId
                        select new RoomUserDto
                        {
                            RoomUserId = ru.RoomUserId,
                            RoomId = ru.RoomId,
                            UserId = ru.UserId,
                            Status = ru.Status
                        };
            RoomUserDto roomUser = query.FirstOrDefault();

            return roomUser;
        }

        public RoomUserDto Insert(RoomUserDto roomUser)
        {
            RoomUser newRoomUser = new RoomUser
            {
                RoomId = roomUser.RoomId,
                UserId = roomUser.UserId,
                Status = roomUser.Status
            };

            _context.RoomUsers.Add(newRoomUser);
            _context.SaveChanges();

            roomUser.RoomUserId = newRoomUser.RoomUserId;
            return roomUser;
        }

        public RoomUserDto Update(RoomUserDto roomUser)
        {
            RoomUser roomUserToUpdate = _context.RoomUsers.Where(w => w.RoomId == roomUser.RoomId && w.UserId == roomUser.UserId).FirstOrDefault();
            roomUserToUpdate.Status = roomUser.Status;
            _context.RoomUsers.Update(roomUserToUpdate);
            _context.SaveChanges();

            roomUser.RoomUserId = roomUserToUpdate.RoomUserId;
            return roomUser;
        }
    }
}
