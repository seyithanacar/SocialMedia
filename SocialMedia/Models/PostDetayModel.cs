using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class PostDetayModel
    {
        public Post SinglePost { get; set; }
        public List<Post> PostList { get; set; }
        public Comment AddCommnet { get; set; } 
        public List<Comment> CommentList { get; set; }
    }
}