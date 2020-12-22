using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FindAFriend.Models
{
    public class FriendRequests
    {
        [Key]
        public virtual int RequestID { get; set; }
        public virtual String Sender { get; set; }
        public virtual String Recipient { get; set; }
    }
}
