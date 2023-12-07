using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class FollowRequest
    {
        [Key]
        public int FollowRequestId { get; set; }
        public int IstekGönderenUserId { get; set; }
        public virtual User IstekGönderen { get; set; }

        public int IstekAlanUserId { get; set; }
        public virtual User IstekAlan{ get; set; }
    }
}