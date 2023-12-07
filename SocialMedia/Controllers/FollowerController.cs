using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class FollowerController : Controller
    {
        // GET: Follower
        Context c = new Context();
        public ActionResult Followers(int id)
        {
            var values = c.Followings.Where(x => x.TakipEdilenUserId == id).Select(x => x.TakipciUserId).ToList();
            var valuess = c.Users.Where(x => values.Contains(x.UserID)).ToList();
            ViewBag.userid = id;

            return View(valuess);
        }
        public ActionResult FollowRequests(int id)
        {
            ViewBag.userid = id;
            var values = c.FollowRequests.Where(x=>x.IstekAlanUserId==id).Select(x=>x.IstekGönderenUserId).ToList();
            var valuess = c.Users.Where(x => values.Contains(x.UserID)).ToList();
            return View(valuess);
        }
    }
}