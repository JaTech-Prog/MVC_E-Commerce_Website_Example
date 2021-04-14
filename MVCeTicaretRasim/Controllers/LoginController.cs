using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCeTicaretRasim.Models;

namespace MVCeTicaretRasim.Controllers
{
    public class LoginController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {
            string username = frm["userName"];
            string password = frm["password"];

            Customer customer = db.Customers.Where(x => x.UserName == username && x.Password == password).FirstOrDefault();

            if (customer != null)
            {
                Session["OnlineKullanici"] = customer.UserName;
                TemporaryUserData.OnlineUserID = customer.CustomerID;
                customer.LastLogin = DateTime.Now;
                return RedirectToAction("Index", "Home");
            }


            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection frm)
        {
            string userName = frm["UserName"];

            Customer customer = db.Customers.Where(x => x.UserName == userName).FirstOrDefault();

            if (customer == null)
            {
                customer = new Customer();
                customer.FirstName = frm["FirstName"];
                customer.LastName = frm["LastName"];
                customer.UserName = userName;
                customer.Password = frm["Password"];
                customer.Age = int.Parse(frm["Age"]);
                customer.Gender = frm["Gender"] == "on" ? true : false;
                customer.BirthDate = DateTime.Parse(frm["BirthDate"]);
                customer.Email = frm["Email"];
                customer.Mobile1 = frm["Mobile"];
                customer.Address1 = frm["Address"];
                customer.CreatedDate = DateTime.Now.Date;
                customer.LastLogin = DateTime.Now;

                db.Customers.Add(customer);
                db.SaveChanges();

                Session["OnlineKullanici"] = customer.UserName;
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["OnlineKullanici"] = null;
            TemporaryUserData.OnlineUserID = 0;
            return RedirectToAction("Index", "Home");
        }
    }
}