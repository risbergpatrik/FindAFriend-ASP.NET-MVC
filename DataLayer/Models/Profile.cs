using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Models
{
    public class Profile
    {
        public virtual int ProfileID { get; set; }
        public virtual String Name { get; set; }
        public virtual DateTime Birthday { get; set; }
        public virtual String PictureUrl { get; set; }
        public virtual String Description { get; set; }
        public virtual String City { get; set; }
        public virtual String UserID { get; set; }
        
    }
}
