using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCeTicaretRasim.Models;

namespace MVCeTicaretRasim.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            //
            //Anasayfaya random 4 tane ürün verir.

            TempData["Aksiyon"] = db.Products.Where(x => x.SubCategoryID == 1).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
            TempData["Spor"] = db.Products.Where(x => x.SubCategoryID == 2).OrderBy(x => Guid.NewGuid()).Take(4).ToList();
            TempData["Strateji"] = db.Products.Where(x => x.SubCategoryID == 3).OrderBy(x => Guid.NewGuid()).Take(4).ToList();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}