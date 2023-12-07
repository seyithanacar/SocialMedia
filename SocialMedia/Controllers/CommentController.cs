using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class CommentController : Controller
    {
        Context c = new Context();

        [HttpGet]
        public ActionResult AddComment()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            
            comment.CommentDate = DateTime.Now;
            comment.UserId = 1;
            c.Comments.Add(comment);
            c.SaveChanges();
            
            return RedirectToAction("PostDetay","Post");
        }
        public ActionResult CommentList(int  id)
        {
            var values = c.Comments.Where(x => x.PostId == id).OrderByDescending(x => x.CommentDate).ToList();

           
            return View(values);
        }
    }
}