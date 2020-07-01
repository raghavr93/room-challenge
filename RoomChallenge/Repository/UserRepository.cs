using RoomChallenge.Data;
using RoomChallenge.Model;
using RoomChallenge.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomChallenge.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool DeleteUser(User user)
        {
            _db.Users.Remove(user);
            return Save();
        }

        public User GetUser(string Username)
        {
            return _db.Users.FirstOrDefault(a => a.Username == Username);
        }

        public User GetUser(int id)
        {
            return _db.Users.FirstOrDefault(a => a.Id == id);
        }

        public ICollection<User> GetUsers()
        {
            return _db.Users.OrderBy(a => a.Username).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _db.Users.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _db.Users.Any(a => a.Id == id);
        }
    }
}
