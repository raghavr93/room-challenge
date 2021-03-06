﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoomChallenge.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string MobileToken { get; set; }
        [NotMapped]
        public string Token { get; set; }

        public ICollection<UserRoom> UserRooms { get; } = new List<UserRoom>();
    }
}
