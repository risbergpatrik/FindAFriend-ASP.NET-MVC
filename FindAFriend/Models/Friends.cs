using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FindAFriend.Models
{
    public class Friends
    {
        [Key]
        public virtual int FriendshipID { get; set; }
        public virtual String User { get; set; }
        public virtual String FriendWith { get; set; }
    }
}
