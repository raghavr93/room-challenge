using Microsoft.EntityFrameworkCore.Migrations.Operations;
using RoomChallenge.Model;
using RoomChallenge.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomChallenge.Repository.IRepository
{
    public interface IRoomRepository
    {
        bool CreateRoom(Room roomObj);
        Room GetRoomByRoomId(Guid Id);
        bool UpdateRoom(Room room);
        public bool RoomExists(Guid Id);
        public bool AddToRoom(UserRoom userRoom);
        public bool DeleteFromRoom(UserRoom userRoom);
        public bool MemberExists(UserRoom obj);
        public Room GetRoomInfo(Guid guid);
        public List<AllRoomsDto> AllRooms(string username);
        public bool Save();
    }
}
