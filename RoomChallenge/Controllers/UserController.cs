using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RoomChallenge.Model;
using RoomChallenge.Model.Dto;
using RoomChallenge.Repository.IRepository;

namespace RoomChallenge.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _uRepo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository uRepo,IMapper mapper)
        {
            _uRepo = uRepo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var objList = _uRepo.GetUsers();

            var objDto = new List<UserDto>();

            foreach (var obj in objList)
            {
                objDto.Add(_mapper.Map<UserDto>(obj));
            }

            return Ok(objDto);
        }

        [AllowAnonymous]
        [HttpGet("{username}", Name = "GetUser")]
        public IActionResult GetUsers(string username)
        {
            var Obj = _uRepo.GetUser(username);

            if (Obj == null)
            {
                return NotFound();
            }

            var objDto = _mapper.Map<UserDto>(Obj);

            return Ok(objDto);
        }

        [HttpPatch("update")]
        public IActionResult UpdateUser([FromBody] UserDto userDto)
        {

            //Decode payload from the token
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;

            var idClaim = claim.Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault().Value;

            if ((userDto == null ) || Convert.ToInt32(idClaim) != userDto.Id)
            {
                return BadRequest(ModelState);
            }
            var userObj = _mapper.Map<User>(userDto);

            if (!_uRepo.UpdateUser(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong while updating User {userObj.Username}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("delete")]
        public IActionResult DeleteUser()
        {
            //Decode payload from the token
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;

            var idClaim = claim.Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault().Value;
            
            if (!_uRepo.UserExists(Convert.ToInt32(idClaim)))
            {
                return NotFound();
            }
            var userObj = _uRepo.GetUser(Convert.ToInt32(idClaim));

            if (!_uRepo.DeleteUser(userObj))
            {
                ModelState.AddModelError("", $"Something went wrong while deleting User {userObj.Username}");
                return StatusCode(500, ModelState);
            }
           
            return NoContent();
        }
    }
}
