using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RoomChallenge.Model
{
    public class Room
    {
        public string Name { get; set; }
        [Key]
        public  Guid Guid { get; set; }
        [Range(5,50)]
        [DefaultValue(5)]
        public int Capacity { get; set; }
        public int HostId { get; set; }

        [ForeignKey("HostId")]
        public User User { get; set; }

        public ICollection<UserRoom> UserRooms { get; } = new List<UserRoom>();
    }
}
