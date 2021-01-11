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
        [Required(ErrorMessage = "Textfält är tomt")]
        [StringLength(60, ErrorMessage = "Håll dig under 60 tecken!")]
        public virtual string Text { get; set; }
        public virtual String Sender { get; set; }
        public virtual String Recipient { get; set; }
        public virtual DateTime TimeSent { get; set; }
    }
}
