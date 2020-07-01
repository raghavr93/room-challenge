using RoomChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomChallenge.Repository.IRepository
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(string Username);
        User GetUser(int id);
        bool UserExists(int id);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
