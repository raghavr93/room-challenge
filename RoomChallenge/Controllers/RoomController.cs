using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoomChallenge.Model;
using RoomChallenge.Model.Dto;
using RoomChallenge.Repository.IRepository;

namespace RoomChallenge.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomRepository _roomRepo;
        private readonly IMapper _mapper;

        public RoomController(IRoomRepository roomRepo,IMapper mapper)
        {
            _roomRepo = roomRepo;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult CreareRoom([FromBody] RoomDto roomDto)
        {
            if (!ModelState.IsValid || roomDto == null)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;

            var idClaim = claim.Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault().Value;

            var roomObj = _mapper.Map<Room>(roomDto);
            roomObj.HostId = Convert.ToInt32(idClaim);

            
            if (!_roomRepo.CreateRoom(roomObj))
            {
                ModelState.AddModelError("", $"Something went wrong while creating room {roomObj.Name}");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }

        [HttpPatch("changehost")]
        public IActionResult UpdateHost([FromBody] HostUpdateDto hostUpdateDto)
        {
            if (!ModelState.IsValid || hostUpdateDto == null)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;

            var idClaim = claim.Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault().Value;

            var roomObj =_roomRepo.GetRoomByRoomId(hostUpdateDto.Guid);

            if(roomObj.HostId != Convert.ToInt32(idClaim))
            {
                return BadRequest("Action Not Allowd");
            }
            else
            {
                roomObj.HostId = hostUpdateDto.HostId;
                _roomRepo.UpdateRoom(roomObj);
            }

            return NoContent();
        }

        [HttpPost("addremove")]
        public IActionResult AddRemove([FromBody] GuidDto addremoveDto)
        {
            if (!ModelState.IsValid || addremoveDto == null)
            {
                return BadRequest(ModelState);
            }

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IEnumerable<Claim> claim = identity.Claims;

            var idClaim = claim.Where(x => x.Type == ClaimTypes.Name)
                .FirstOrDefault().Value;

            if (_roomRepo.RoomExists(addremoveDto.Guid))
            {
                UserRoom objUserRoom = new UserRoom();

                objUserRoom.Guid = addremoveDto.Guid;
                objUserRoom.Id = Convert.ToInt32(idClaim);

                if (_roomRepo.MemberExists(objUserRoom))
                {
                    if (_roomRepo.DeleteFromRoom(objUserRoom))
                        return StatusCode(201);
                    else
                        return BadRequest();
                }
                else
                {
                    if (_roomRepo.AddToRoom(objUserRoom))
                        return StatusCode(201);
                    else
                        return BadRequest();
                }
            }
            else
                return BadRequest();
        }
        [AllowAnonymous]
        [HttpGet("roominfo/{guid}")]
        public IActionResult GetRoomInfo(Guid guid)
        {
            var Obj = _roomRepo.GetRoomInfo(guid);

            if (Obj == null)
            {
                return NotFound();
            }

            return Ok(Obj);
        }

        [HttpGet("allrooms/{username}")]
        public IActionResult GetAllRooms(string username)
        {
            var Obj = _roomRepo.AllRooms(username);

            if (Obj == null)
            {
                return NotFound();
            }

            return Ok(Obj);
        }




    }
}
