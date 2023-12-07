using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageDate { get; set; }

        // Mesajın gönderen kullanıcı kimliği
        public int SenderUserId { get; set; }

        // Mesajın alıcı kullanıcı kimliği
        public int ReceiverUserId { get; set; }

        // Mesajı gönderen kullanıcıyı temsil eden ilişki
        public virtual User Sender { get; set; }

        // Mesajı alan kullanıcıyı temsil eden ilişki
        public virtual User Receiver { get; set; }

    }
}