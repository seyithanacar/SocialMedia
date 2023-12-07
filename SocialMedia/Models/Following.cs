using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Following
    {
        [Key]
        public int FollowingId { get; set; }

        public int TakipciUserId { get; set; }
        public virtual User Takipci { get; set; }

        public int TakipEdilenUserId { get; set; }
        public virtual User TakiEdilen { get; set; }

    }
}