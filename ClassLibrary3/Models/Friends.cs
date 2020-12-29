using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Friends
    {
        public virtual int FriendshipID { get; set; }
        public virtual String User { get; set; }
        public virtual String FriendWith { get; set; }
    }
}
