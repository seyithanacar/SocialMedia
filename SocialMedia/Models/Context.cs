using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{

    public class Context : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<FollowRequest> FollowRequests { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // User sınıfı ile SentMessages ve ReceivedMessages arasındaki ilişkiyi tanımlama
            modelBuilder.Entity<User>()
                .HasMany(u => u.SentMessages) // Bir kullanıcının gönderdiği mesajlar koleksiyonu
                .WithRequired(m => m.Sender)  // Mesajın gönderen kullanıcı
                .HasForeignKey(m => m.SenderUserId) // Mesajın gönderen kullanıcı kimliği
                .WillCascadeOnDelete(false); // Kullanıcı silindiğinde bu ilişkinin otomatik olarak silinmeyeceğini belirtme

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages) // Bir kullanıcının aldığı mesajlar koleksiyonu
                .WithRequired(m => m.Receiver)  // Mesajın alıcı kullanıcı
                .HasForeignKey(m => m.ReceiverUserId) // Mesajın alıcı kullanıcı kimliği
                .WillCascadeOnDelete(false); // Kullanıcı silindiğinde bu ilişkinin otomatik olarak silinmeyeceğini belirtme

            // Comment modelli ile Post modelli arasındaki ilişkiyi ele alarak kaskat silme ayarı
            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.Post) // Comment, Post'a bağımlıdır
                .WithMany(p => p.Comments) // Post, birden çok Comment içerebilir
                .HasForeignKey(c => c.PostId) // Comment tablosundaki PostId ile ilişkilendirilir
                .WillCascadeOnDelete(false); // Kaskat silme ayarı

            // Comment modelli ile Post modelli arasındaki ilişkiyi ele alarak kaskat silme ayarı
            modelBuilder.Entity<Comment>()
                .HasRequired(c => c.User) // Comment, Post'a bağımlıdır
                .WithMany(p => p.Comments) // Post, birden çok Comment içerebilir
                .HasForeignKey(c => c.UserId) // Comment tablosundaki PostId ile ilişkilendirilir
                .WillCascadeOnDelete(false); // Kaskat silme ayarı

            modelBuilder.Entity<FollowRequest>()
                .HasRequired(fr => fr.IstekGönderen)
                .WithMany()
                .HasForeignKey(fr => fr.IstekGönderenUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Following>()
                .HasRequired(fr => fr.TakiEdilen)
                .WithMany()
                .HasForeignKey(fr => fr.TakipEdilenUserId)
                .WillCascadeOnDelete(false);


        }

    }
}