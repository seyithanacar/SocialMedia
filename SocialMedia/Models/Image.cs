using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Image
    {
        public int ImageId { get; set; } 
        public string ImageAdress { get; set; }
        public string ImageName { get; set; }
        public ICollection<Admin> Admin { get; set; }  
        public ICollection<Post> Post { get; set; }  

    }
}