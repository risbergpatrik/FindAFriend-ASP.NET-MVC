using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FindAFriend.Models
{
    public class Profile
    {
        [Key]
        public virtual int ProfileID { get; set; }
        public virtual String Name { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual String PictureUrl { get; set; }
        public virtual String Description { get; set; }
        public virtual String City { get; set; }
        public virtual String UserID { get; set; }
        
    }
}
