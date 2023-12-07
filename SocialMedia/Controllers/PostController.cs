using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class PostController : Controller
    {
        Context c = new Context();


        
        public ActionResult PostDetay(int id)
        {

            var post = new PostDetayModel
            {
                SinglePost = c.Posts.Find(id),

                CommentList = c.Comments.Where(x => x.PostId == id).OrderByDescending(x => x.CommentDate).ToList()
            };

            var values = new PostDetayModel
            {
            };

            ViewBag.postid = id;
            return View(post);
        }
        [HttpPost]
        public ActionResult PostDetay(string id)
        {

            ViewBag.id = id;
            return RedirectToAction("AddComment",id);
        }
        public ActionResult PostCommentList(int id)
        {
            var values = new PostDetayModel
            {
                CommentList = c.Comments.Where(x => x.PostId == id).OrderByDescending(x => x.CommentDate).ToList()
            };

            return View(values);
        }
       
        
        
        public ActionResult AddComment(PostDetayModel comment,int id )
        {
            var cm = new Comment();
            cm.CommentDate = DateTime.Now;
            cm.PostId = id;
            cm.UserId = (int)Session["userid"];
            c.Comments.Add(cm);
            c.SaveChanges();

            return RedirectToAction("PostDetay", "Post");
        }

    }
}