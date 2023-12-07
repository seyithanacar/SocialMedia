using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        Context c = new Context();
        public ActionResult Messages()
        {


            var values = c.Messages
           .GroupBy(m => m.SenderUserId) // Mesajları gönderen kullanıcıya göre gruplandır
           .Select(group => new
           {
               SenderUserId = group.Key,
               LastMessage = group.OrderByDescending(m => m.MessageDate).FirstOrDefault()
               
           })
           .Where(item => item.LastMessage != null)
           .Select(item => new
           {
               UserName = item.LastMessage.Sender.UserName, // Gönderen kullanıcı adı
               UserSurName = item.LastMessage.Sender.UserSurname, // Gönderen kullanıcı soyadı
               UserId = item.LastMessage.Sender.UserID, // Gönderen kullanıcı id
               LastMessageContent = item.LastMessage.MessageContent // Son mesaj içeriği
           })
           .ToList();

            return View(values.Select(item => (item.UserName, item.LastMessageContent,item.UserId,item.UserSurName))
                .ToList());
        }
        public ActionResult MesajYaz()
        {
            int id = 2;
            var mesajlar = c.Messages.Where(x=>x.SenderUserId==1 || x.ReceiverUserId==id).OrderByDescending(x=>x.MessageDate).Take(10).ToList();
            ViewBag.sender = "ererere";
            return View( mesajlar);
        }
        [HttpPost]
        public ActionResult MesajYaz(int id,String mssg)
        {
            Message m = new Message();
            m.MessageContent = mssg;
            m.MessageDate = DateTime.Now;
            m.SenderUserId = 1;
            m.ReceiverUserId = 2;
            c.Messages.Add(m);
            c.SaveChanges();
            return MesajYaz();
        }
    }
}