using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public int CommentReportCount { get; set; }
        public int CommentLikeCount { get; set; }
        public int CommentDislikeCount { get; set; }
        public int PostId { get; set; }
        [ForeignKey("PostId")] // PostId ile ilişkilendirme
        public virtual Post Post { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")] // UserId ile ilişkilendirme
        public virtual User User { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}