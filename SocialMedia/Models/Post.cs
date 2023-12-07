using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public DateTime? PostDate{ get; set; }
        public string PostContent { get; set; }
        public string PostReportCount { get; set; }
        public int PostLikeCount { get; set; }
        public int PostDislikeCount { get; set; }
        public int? ImageId { get; set; }  
        public virtual Image Images { get; set; }  
        public int UserId { get; set; }    
        public virtual User User { get; set; }  
        public ICollection<Report> Reports { get; set; }
        public ICollection<Comment> Comments { get; set; }
                
    }
}