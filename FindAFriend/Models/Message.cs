using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FindAFriend.Models
{
    public class Message
    {
        [Key]
        public virtual int ID { get; set; }
        public virtual String Sender { get; set; }
        public virtual String Recipient { get; set; }

        public virtual DateTime TimeSent { get; set; }
    }
}
