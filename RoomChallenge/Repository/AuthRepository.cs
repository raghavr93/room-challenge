using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RoomChallenge.Data;
using RoomChallenge.Model;
using RoomChallenge.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoomChallenge.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly AppSettings _appSettings;

        public AuthRepository(ApplicationDbContext db,IOptions<AppSettings> appSettings)
        {
            _db = db;
            _appSettings = appSettings.Value;
        }
        public User Authenticate(string username, string password)
        {
            var user = _db.Users.FirstOrDefault(x => x.Username == username && x.Password == password);

            //User was not found 
            if (user == null)
            {
                return null;
            }

            //User found generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials
                                (new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";
            return user;
        }

        public bool IsUniqueUser(string username)
        {
            var user = _db.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
                return true;

            return false;
        }

        public User Register(string username, string password, string mobileToken)
        {
            User userObj = new User()
            {
                Username = username,
                Password = password,
                MobileToken = mobileToken
            };

            _db.Users.Add(userObj);
            _db.SaveChanges();

            userObj.Password = "";

            return userObj;
        }
    }
}
