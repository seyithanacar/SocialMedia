using SocialMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMedia.Controllers
{
    public class AdminController : Controller
    {
        Context c = new Context();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin a)
        {
            var admin = c.Admins.Find(a.AdminMail);
            if (admin.AdminMail==a.AdminMail && admin.AdminPassword==a.AdminPassword)
            {
                Session["ad_soyad"] =admin.AdminName+" "+admin.AdminSurname;
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
            
        }
        public ActionResult Profil()
        {
            var values = c.Admins.Find(1);
            return View(values);
        }
        public ActionResult UsersList()
        {
            var values = c.Users.ToList();
            return View(values);
        }
        public ActionResult ReportList()
        {
            var values = c.Reports.ToList();
            return View(values);
        }

    }
}