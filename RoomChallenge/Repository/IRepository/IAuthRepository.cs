using RoomChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomChallenge.Repository.IRepository
{
    public interface IAuthRepository
    {
        bool IsUniqueUser(string username);
        User Authenticate(string username, string password);
        User Register(string username, string password, string mobileToken);
    }
}
