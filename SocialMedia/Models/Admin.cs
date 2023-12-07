using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }
        public string AdminMail { get; set; }
        public string AdminName { get; set; }
        public string AdminSurname{ get; set; }
        public string AdminPassword{ get; set; }

        public int ImageId { get; set; }
        public virtual Image ProfileImage { get; set; }
    }
}