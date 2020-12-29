using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Message
    {
        public virtual int ID { get; set; }
        public virtual String Sender { get; set; }
        public virtual String Recipient { get; set; }

        public virtual DateTime TimeSent { get; set; }
    }
}
