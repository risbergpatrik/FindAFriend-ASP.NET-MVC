/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindAFriend.Models
{
    public class File
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
    }

    public class BufferedSingleFileUploadDbModel : File
    {

        [BindProperty]
        public BufferedSingleFileUploadDb FileUpload { get; set; }
    }

    public class BufferedSingleFileUploadDb
    {
        [Required]
        [Display(Name = "File")]
        public IFormFile FormFile { get; set; }
    }
}*/