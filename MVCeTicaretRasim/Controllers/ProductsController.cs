using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCeTicaretRasim.Models;

namespace MVCeTicaretRasim.Controllers
{
    public class ProductsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Product(int id)
        {
            TempData["ProductList"] = db.Products.Where(x => x.SubCategoryID == id).ToList();

            return View();
        }

        public ActionResult ProductDetail(int id)
        {
            TempData["ProductDetail"] = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
            TempData["Reviews"] = db.Reviews.Where(x => x.ProductID == id && x.IsDeleted == false).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddReview(int id, FormCollection frm)
        {
            Review review = new Review()
            {
                CustomerID = TemporaryUserData.OnlineUserID,
                DateTime = DateTime.Now,
                Email = db.Customers.Find(TemporaryUserData.OnlineUserID).Email,
                IsDeleted = false,
                Name = frm["Name"],
                ProductID = id,
                Rates = int.Parse(frm["Rating"]),
                Reviews = frm["Review"]
            };

            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("ProductDetail", new { id = id });
        }
    }
}