using SignalRAngularChat.Data.Contexts;
using SignalRAngularChat.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SignalRAngularChat.Data.Entities;

namespace SignalRAngularChat.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SignalRAngularChatContext _context;
        public UserRepository(SignalRAngularChatContext context)
        {
            _context = context;
        }
        public UserDto GetByUserName(string userName)
        {
            var query = from u in _context.Users
                        where u.UserName == userName
                        select new UserDto
                        {
                            UserId = u.UserId,
                            UserName = u.UserName
                        };
            UserDto user = query.FirstOrDefault();

            return user;
        }

        public List<UserDto> ListUsersByRoomId(int roomId)
        {
            var query = from u in _context.Users
                        join ru in _context.RoomUsers
                        on u.UserId equals ru.UserId
                        where ru.RoomId == roomId
                        select new UserDto
                        {
                            UserId = u.UserId,
                            UserName = u.UserName
                        };
            List<UserDto> users = query.ToList();

            return users;
        }
        public List<UserDto> ListUsersByRoomIdAndStatus(int roomId, string status)
        {
            var query = from u in _context.Users
                        join ru in _context.RoomUsers
                        on u.UserId equals ru.UserId
                        where ru.RoomId == roomId && ru.Status == status
                        select new UserDto
                        {
                            UserId = u.UserId,
                            UserName = u.UserName
                        };
            List<UserDto> users = query.ToList();

            return users;
        }

        public List<UserDto> ListUsersByRoomIdAndUserIdAndStatus(int roomId, int userId, string status)
        {
            var query = from u in _context.Users
                        join ru in _context.RoomUsers
                        on u.UserId equals ru.UserId
                        where ru.RoomId == roomId && ru.UserId == userId && ru.Status == status
                        select new UserDto
                        {
                            UserId = u.UserId,
                            UserName = u.UserName
                        };
            List<UserDto> users = query.ToList();

            return users;
        }

        public UserDto Insert(UserDto user)
        {
            User newUser = new User { UserName = user.UserName };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            user.UserId = newUser.UserId;
            return user;
        }
    }
}
