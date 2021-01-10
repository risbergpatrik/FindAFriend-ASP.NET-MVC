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
        [Required(ErrorMessage = "Fyll i namn")]
        [StringLength(20, ErrorMessage = "Håll ditt namn under 20 tecken tack!")]
        public virtual String Name { get; set; }
        [Required(ErrorMessage = "Fyll i födelsedatum")]
        public virtual DateTime Birthday { get; set; }
        [Required(ErrorMessage = "Beskrivning tom")]
        [StringLength(1000, ErrorMessage ="Beskriv dig själv med mindre än 1000 tecken. Kom igen")]
        public virtual String Description { get; set; }
        [Required(ErrorMessage = "Fyll i stad")]
        [StringLength(20, ErrorMessage ="Flytta till en stad med ett kortare namn. (mindre än 20 tecken gärna)")]
        public virtual String City { get; set; }
        public virtual String UserID { get; set; }

    }
}
