using RoomChallenge.Data;
using RoomChallenge.Model;
using RoomChallenge.Model.Dto;
using RoomChallenge.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace RoomChallenge.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _db;

        public RoomRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateRoom(Room roomObj)
        {
            _db.Rooms.Add(roomObj);
            return Save();
        }

        public Room GetRoomByRoomId(Guid Id)
        {
            var obj =_db.Rooms.FirstOrDefault(x => x.Guid == Id);

            return obj;
        }

        public bool RoomExists(Guid Id)
        {
            return _db.Rooms.Any(x => x.Guid == Id);
        }

        public bool UpdateRoom(Room roomObj)
        {
            _db.Rooms.Update(roomObj);
            return Save();
        }

        public bool MemberExists(UserRoom obj)
        {
            return _db.UserRooms.Any(x => x.Guid == obj.Guid && x.Id == obj.Id);
        }

        public bool AddToRoom(UserRoom userRoom)
        {
            int membercount = _db.UserRooms.Count(x => x.Guid == userRoom.Guid);

            if (membercount > 50)
                return false;

            _db.UserRooms.Add(userRoom);
            return Save();
        }
        public bool DeleteFromRoom(UserRoom userRoom)
        {
            _db.UserRooms.Remove(userRoom);
            return Save();
        }

        public Room GetRoomInfo(Guid guid)
        {
            var obj = _db.Rooms.FirstOrDefault(x => x.Guid == guid);
            return obj;
        }

        public List<AllRoomsDto> AllRooms(string username)
        {
            var rooms = (from room in _db.Rooms
                         join userroom in _db.UserRooms on room.Guid equals userroom.Guid
                         join usr in _db.Users on userroom.Id equals usr.Id
                         where usr.Username == username
                         select new
                         {
                             allRooms = room.Name
                         });

            return (List<AllRoomsDto>)rooms;
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0 ? true : false;
        }
    }
}
