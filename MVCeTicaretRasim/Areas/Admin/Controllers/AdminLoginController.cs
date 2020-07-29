using MVCeTicaretRasim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCeTicaretRasim.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string adminUserName = frm["adminUserName"];
            string adminPassword = frm["adminPassword"];

            AdminLogin admin = db.AdminLogins.Where(x => x.UserName == adminUserName && x.Password == adminPassword).FirstOrDefault();

            if(TemporaryUserData.OnlineUserID != 0)
            {
                Session["OnlineKullanici"] = null;
                TemporaryUserData.OnlineUserID = 0;
                return RedirectToAction("Index", "AdminHome");
            }

            if (admin != null)
            {
                Session["OnlineAdmin"] = admin.UserName;
                TemporaryUserData.OnlineAdminID = admin.LoginID;
                
                return RedirectToAction("Index", "AdminHome");
            }


            return View();
        }

        public ActionResult Logout()
        {
            Session["OnlineAdmin"] = null;
            TemporaryUserData.OnlineAdminID = 0;
            return RedirectToAction("Index", "Home",new { area=""});
        }
    }
}