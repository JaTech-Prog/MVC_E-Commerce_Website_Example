using MVCeTicaretRasim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCeTicaretRasim.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            if (Session["OnlineAdmin"] != null)
                return View();
            else
                return RedirectToAction("Login", "AdminLogin");
        }
    }
}