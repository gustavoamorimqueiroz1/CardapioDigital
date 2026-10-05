using System;
using System.Collections.Generic;
using System.Text;

namespace CompreAqui.Domain.Models.Entities
{
    public class FaceUser 
    {
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? FacebookId { get; set; }
        public string PictureUrl { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
    
}
