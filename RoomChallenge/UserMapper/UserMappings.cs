using AutoMapper;
using RoomChallenge.Model;
using RoomChallenge.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomChallenge.UserMapper
{
    public class UserMappings:Profile
    {
        public UserMappings()
        {
            CreateMap<User,UserDto>().ReverseMap();
            CreateMap<Room, RoomDto>().ReverseMap();
        }
    }
}
