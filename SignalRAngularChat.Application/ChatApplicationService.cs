using SignalRAngularChat.Data.Repositories;
using SignalRAngularChat.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SignalRAngularChat.Application
{
    public class ChatApplicationService : IChatApplicationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IRoomUserRepository _roomUserRepository;
        public ChatApplicationService(
            IUserRepository userRepository,
            IRoomRepository roomRepository,
            IChatRepository chatRepository,
            IRoomUserRepository roomUserRepository)
        {
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _chatRepository = chatRepository;
            _roomUserRepository = roomUserRepository;
        }
        public UserDto GetUserByUserName(string userName)
        {
            UserDto user = _userRepository.GetByUserName(userName);
            return user;
        }
        public List<UserDto> ListUsersByRoomIdAndUserIdAndStatus(int roomId, int userId, string status)
        {
            List<UserDto> users = _userRepository.ListUsersByRoomIdAndUserIdAndStatus(roomId, userId, status);
            return users;
        }

        public List<UserDto> ListUsersByRoomId(int roomId)
        {
            List<UserDto> users = _userRepository.ListUsersByRoomId(roomId);
            return users;
        }
        public List<UserDto> ListUsersByRoomIdAndStatus(int roomId, string status)
        {
            List<UserDto> users = _userRepository.ListUsersByRoomIdAndStatus(roomId, status);
            return users;
        }
        public UserDto InsertUser(UserDto user)
        {
            return _userRepository.Insert(user);
        }

        public RoomDto GetRoomByRoomId(int roomId)
        {
            RoomDto room = _roomRepository.GetByRoomId(roomId);
            return room;
        }
        public RoomDto GetRoomByRoomName(string roomName)
        {
            RoomDto room = _roomRepository.GetByRoomName(roomName);
            return room;
        }
        public List<RoomDto> ListRooms()
        {
            List<RoomDto> rooms = _roomRepository.ListRooms();
            return rooms;
        }
        public RoomDto InsertRoom(RoomDto room)
        {
            return _roomRepository.Insert(room);
        }

        public List<ChatDto> ListChatByRoomId(int roomId)
        {
            List<ChatDto> rooms = _chatRepository.ListByRoomId(roomId);
            return rooms;
        }
        public ChatDto InsertChat(ChatDto chat)
        {
            return _chatRepository.Insert(chat);
        }

        public RoomUserDto GetRoomUserByRoomIdAndUserId(int roomId, int userId)
        {
            RoomUserDto roomUser = _roomUserRepository.GetByRoomIdAndUserId(roomId, userId);
            return roomUser;
        }
        public RoomUserDto InsertRoomUser(RoomUserDto roomUser)
        {
            return _roomUserRepository.Insert(roomUser);
        }
        public RoomUserDto UpdateRoomUser(RoomUserDto roomUser)
        {
            return _roomUserRepository.Update(roomUser);
        }
    }
}
