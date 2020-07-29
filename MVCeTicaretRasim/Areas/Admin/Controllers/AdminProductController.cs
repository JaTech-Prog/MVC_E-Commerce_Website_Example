using MVCeTicaretRasim.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCeTicaretRasim.Areas.Admin.Controllers
{
    public class AdminProductController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        private bool AdminCheck()
        {
            if (Session["OnlineAdmin"] != null)
                return true;
            else
                return false;
        }

        #region Product
        public ActionResult ListProducts()
        {
            TempData["ProductList"] = db.Products.ToList();


            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        public ActionResult EditProduct()
        {
            TempData["EditProduct"] = db.Products.ToList();

            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        public ActionResult ProductEdit(int id)
        {
            Product product = db.Products.Where(x => x.ProductID == id).FirstOrDefault();

            if (AdminCheck() == true)
            {
                return View(db.Products.Find(id));
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        [HttpPost]
        public ActionResult ProductEdit(int id, FormCollection frm)
        {
            Product product = db.Products.Where(x => x.ProductID == id).FirstOrDefault();

            product.ProductName = frm["ProductName"];
            product.UnitPrice = Convert.ToDecimal(frm["UnitPrice"]);
            product.UnitOnOrder = int.Parse(frm["UnitOnOrder"]);
            product.Size = frm["Size"];
            product.ShortDescription = frm["ShortDescription"];
            product.LongDescription = frm["LongDescription"];
            product.UnitsInStock = int.Parse(frm["UnitsInStock"]);

            db.SaveChanges();

            if (AdminCheck() == true)
            {
                return RedirectToAction("UpdatedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        public ActionResult ProductDelete(int id)
        {
            Product product = db.Products.Where(x => x.ProductID == id).FirstOrDefault();

            db.Products.Remove(product);
            db.SaveChanges();

            if (AdminCheck() == true)
            {
                return RedirectToAction("DeletedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        public ActionResult AddProduct()
        {
            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        [HttpPost]
        public ActionResult AddProduct(FormCollection frm, HttpPostedFileBase ImageUrl)
        {
            string imgYolu;
            Product product = new Product();

            product.ProductName = frm["ProductName"];
            product.UnitPrice = Convert.ToDecimal(frm["UnitPrice"]);
            product.UnitOnOrder = 0;
            product.Size = frm["Size"];
            product.SupplierID = Convert.ToInt32(frm["SupplierID"]);
            product.ProductAvailable = true;
            product.ShortDescription = frm["ShortDescription"];
            product.AddBadge = false;
            product.LongDescription = frm["LongDescription"];
            product.UnitsInStock = int.Parse(frm["UnitsInStock"]);
            product.SubCategoryID = int.Parse(frm["SubCategoryID"]);
            product.AltText = frm["AltText"];

            switch (product.SubCategoryID)
            {
                case 1:
                    imgYolu = "Aksiyon";
                    break;
                case 2:
                    imgYolu = "Spor";
                    break;
                case 3:
                    imgYolu = "Strateji";
                    break;
                default:
                    imgYolu = "";
                    break;
            }

            var fileName = Path.GetFileName(ImageUrl.FileName);

            product.ImageUrl = "~/Images/" + imgYolu + "/" + fileName;

            db.Products.Add(product);
            db.SaveChanges();

            if (AdminCheck() == true)
            {
                return RedirectToAction("ProductAddedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }

        #endregion

        #region Reviews

        public ActionResult ListReviews()
        {
            TempData["ReviewList"] = db.Reviews.ToList();

            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        public ActionResult EditReview()
        {
            TempData["ReviewList"] = db.Reviews.ToList();

            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        public ActionResult ReviewEdit(int id)
        {
            Review review = db.Reviews.Where(x => x.ReviewID == id).FirstOrDefault();

            if (AdminCheck() == true)
            {
                return View(db.Reviews.Find(id));
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }

        [HttpPost]
        public ActionResult ReviewEdit(FormCollection frm, int id)
        {
            Review review = db.Reviews.Where(x => x.ReviewID == id).FirstOrDefault();

            review.Reviews = frm["Reviews"];
            review.Rates = Convert.ToInt32(frm["Rates"]);

            db.SaveChanges();


            if (AdminCheck() == true)
            {
                return RedirectToAction("ReviewUpdatedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        public ActionResult DeleteReview(int id)
        {
            Review review = db.Reviews.Where(x => x.ReviewID == id).FirstOrDefault();

            db.Reviews.Remove(review);
            db.SaveChanges();

            if (AdminCheck() == true)
            {
                return RedirectToAction("ReviewDeletedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }
        #endregion

        #region SuccessPages

        public ActionResult UpdatedSuccessfully()
        {
            return View();
        }

        public ActionResult DeletedSuccessfully()
        {
            return View();
        }

        public ActionResult ProductAddedSuccessfully()
        {
            return View();
        }

        public ActionResult ReviewUpdatedSuccessfully()
        {
            return View();
        }

        public ActionResult ReviewDeletedSuccessfully()
        {
            return View();
        }
        #endregion
    }
}