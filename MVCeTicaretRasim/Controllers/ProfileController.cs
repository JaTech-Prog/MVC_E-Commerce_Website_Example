using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCeTicaretRasim.Models;
namespace MVCeTicaretRasim.Controllers
{
    public class ProfileController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult UpdateProfile()
        {
            return View(db.Customers.Find(TemporaryUserData.OnlineUserID));
        }

        [HttpPost]
        public ActionResult UpdateProfile(FormCollection frm)
        {
            Customer customer = db.Customers.Find(TemporaryUserData.OnlineUserID);
            customer.FirstName = frm["FirstName"];
            customer.LastName = frm["LastName"];
            customer.Password = frm["Password"];
            customer.Age = int.Parse(frm["Age"]);
            customer.Address1 = frm["Address"];
            customer.Mobile1 = frm["Mobile1"];
            customer.BirthDate = DateTime.Parse(frm["BirthDate"]);


            db.SaveChanges();
            return RedirectToAction("UpdatedSuccessfully");
        }

        public ActionResult UpdatedSuccessfully()
        {
            return View();
        }
    }
}