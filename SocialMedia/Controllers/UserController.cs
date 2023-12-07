using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class UserController : Controller
    {

        Context c = new Context();
        public ActionResult Index()
        {

            var values = new PostDetayModel
            {

                PostList = c.Posts.OrderByDescending(x => x.PostDate).ToList()

            };
            return View(values);
            
        }
        [HttpPost]
        public ActionResult PostEkle(Post p)
        {

            if (Request.Files.Count>0)
            {
                Image img = new Image();
                img.ImageName = Path.GetFileName(Request.Files[0].FileName);

                string adres = Server.MapPath("~/Image/" + img.ImageName);
                Request.Files[0].SaveAs(adres);
                img.ImageAdress = adres;
                c.Images.Add(img);
                c.SaveChanges();

                int sonimgid = img.ImageId;

                p.ImageId = sonimgid;
            }

            p.PostDate = DateTime.Now;
            p.UserId = (int)Session["userid"];
            c.Posts.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index", "User");
        }



        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User u)
        {
            var user = c.Users.FirstOrDefault(x => x.UserMail == u.UserMail);
            if (user.UserMail != null &&user.UserMail == u.UserMail && u.UserPassword == user.UserPassword)
            {
                Session["ad_soyad"] = user.UserName + " " + user.UserSurname;
                Session["userid"] = user.UserID;
                return RedirectToAction("Index", "User");

            }
            else
            {
                TempData["msg"] = "<script>alert('KULLANICI ADI YADA ŞİFRE HATALI');</script>";

                return View();
            }
        }
        [HttpGet]
        public ActionResult KayıtOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KayıtOl(User u)
        {
            if (Request.Files.Count > 0)
            {
                Image img = new Image();
               
                img.ImageName = Path.GetFileName(Request.Files[0].FileName);
                
                string adres = Server.MapPath("~/Image/" + img.ImageName);
                if (Request.Files[0].FileName!=null)
                Request.Files[0].SaveAs(adres);
                img.ImageAdress = adres;
                c.Images.Add(img);
                c.SaveChanges();

                // image tablosunaki resmin idsi
                int sonresimid= img.ImageId;

                u.ImageId = sonresimid;
            }
            c.Users.Add(u);
            c.SaveChanges();

            return RedirectToAction("Login");
        }

        public new ActionResult Profile(int id)
        {
            var values = c.Users.FirstOrDefault(x => x.UserID == id);
            if (values.UserID == (int)Session["userid"]&& Session["userid"]!=null)
            {
                return RedirectToAction("MyProfil");
            }

            int myuserId = (int)Session["userid"];

            if (c.FollowRequests.FirstOrDefault(x => x.IstekGönderenUserId == id && x.IstekAlanUserId == myuserId) != null)
            //takip isteği alındıysa
            {
                ViewBag.ctrl = 2;
            }

            else if (c.FollowRequests.FirstOrDefault(x => x.IstekAlanUserId == id && x.IstekGönderenUserId == myuserId) != null)
            //takip isteği gönderildiyse
            {
                ViewBag.ctrl = 1;
            }

            else if (c.Followings.FirstOrDefault(x => x.TakipciUserId == id && x.TakipEdilenUserId == myuserId) != null)
            //takipleşiliyorsa
            {
                ViewBag.ctrl = 3;
            }
            else if (c.Followings.FirstOrDefault(x => x.TakipciUserId == myuserId && x.TakipEdilenUserId == id) != null)
            //takipleşiliyorsa
            {
                ViewBag.ctrl = 3;
            }
            else
            //takip isteği ve takipleşme yoksa
            {
                ViewBag.ctrl = 0;

            }
            ViewBag.myid = myuserId;
            ViewBag.userid = id;
            return View(values);
        }
        [HttpPost]
        public new ActionResult Profile(int followerid, int myid, int statu)
        {
            return TakipDurumu(followerid, myid, statu);
        }

        public ActionResult TakipDurumu(int followerid, int myid, int statu)
        {
            if (statu == 0)//takip isteği yoksa takip isteği ekle
            {
                FollowRequest f = new FollowRequest();
                f.IstekGönderenUserId = myid;
                f.IstekAlanUserId= followerid;
                c.FollowRequests.Add(f);
                c.SaveChanges();
            }
            else if (statu == 2)//takip isteği alındıysa isteği sil takip ilişkisi kur
            {
                var ct = c.FollowRequests.FirstOrDefault(x => x.IstekAlanUserId == myid && x.IstekGönderenUserId == followerid);

                var s = ct;
                c.FollowRequests.Remove(ct);

                Following f = new Following();
                f.TakipEdilenUserId = myid;
                f.TakipciUserId = followerid;
                c.Followings.Add(f);
                c.SaveChanges();
            
            }else if (statu == 1 )//takip isteği gönderildiyse isteği sil takip ilişkisi kur
            {
                var ct = c.FollowRequests.FirstOrDefault(x => x.IstekAlanUserId == followerid && x.IstekGönderenUserId == myid);

                var s = ct;
                c.FollowRequests.Remove(ct);
                c.SaveChanges();
            }
            else if (statu == 3)//takipleşiliyorsa takipten çık
            {
                var f = c.Followings.FirstOrDefault(x => x.TakipciUserId == followerid && x.TakipEdilenUserId == myid);
                if (f == null)
                    f = c.Followings.FirstOrDefault(x => x.TakipciUserId == myid && x.TakipEdilenUserId == followerid);

                c.Followings.Remove(f);
                c.SaveChanges();

            }
            return RedirectToAction("Profile",followerid);
        }
    }


}