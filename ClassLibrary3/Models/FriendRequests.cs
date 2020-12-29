using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class FriendRequests
    {
        public virtual int RequestID { get; set; }
        public virtual String Sender { get; set; }
        public virtual String Recipient { get; set; }
    }
}
