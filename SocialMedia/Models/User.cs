using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string UserMail { get; set; }
        public string UserSurname   { get; set; }
        public string UserName   { get; set; }
        public string UserPassword   { get; set; }
        public int ImageId { get; set; }
        public virtual Image Image { get; set; }    

        public ICollection<Post> Posts { get; set; }
       
        // Bir kullanıcının gönderdiği mesajları temsil eden koleksiyon
        public virtual ICollection<Message> SentMessages { get; set; }

        // Bir kullanıcının aldığı mesajları temsil eden koleksiyon
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}