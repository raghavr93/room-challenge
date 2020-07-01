using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomChallenge.Model;
using RoomChallenge.Repository.IRepository;

namespace RoomChallenge.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationDto model)
        {
            var user = _authRepo.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect." });
            }
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDto model)
        {
            bool isUserNameUnique = _authRepo.IsUniqueUser(model.Username);

            if (!isUserNameUnique)
            {
                return BadRequest(new { message = "User Already Exists!" });
            }
            var user = _authRepo.Register(model.Username, model.Password, model.MobileToken);

            if (user == null)
            {
                return BadRequest(new { message = "Error while registering!" });
            }

            return Ok();

        }
    }
}
