using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomChallenge.Model
{
    public class UserRoom
    {
        public int Id { get; set; }
        public User User { get; set; }

        public Guid Guid { get; set; }
        public Room Room { get; set; }
    }
}
