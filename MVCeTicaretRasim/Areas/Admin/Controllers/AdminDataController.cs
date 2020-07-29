using MVCeTicaretRasim.Areas.Admin.Models;
using MVCeTicaretRasim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCeTicaretRasim.Areas.Admin.Controllers
{
    public class AdminDataController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        private bool AdminCheck()
        {
            if (Session["OnlineAdmin"] != null)
                return true;
            else
                return false;
        }

        public ActionResult LastTenSales()
        {
            TempData["LastTen"] = db.Orders.OrderByDescending(x => x.OrderDate).Take(10).ToList();

            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }

        public ActionResult LastTwoMonthsSales()
        {
            DateTime date = new DateTime();
            date = DateTime.Now.AddMonths(-2);

            TempData["LastTwoMonths"] = db.Orders.Where(x => x.OrderDate >= date).OrderByDescending(x => x.OrderDate).ToList();

            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }

        }

        public ActionResult TopSellingProducts()
        {
            TempData["TopSelling"] = db.Products.OrderByDescending(x => x.UnitOnOrder).Take(5).ToList();

            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }


        #region Users

        public ActionResult ListUsers()
        {
            TempData["UserList"] = db.Customers.ToList();


            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }        

        public ActionResult EditUsers()
        {
            TempData["EditUser"] = db.Customers.ToList();

            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }        

        public ActionResult UserEdit(int id)
        {

            Customer customer = db.Customers.Where(x => x.CustomerID == id).FirstOrDefault();


            if (AdminCheck() == true)
            {
                return View(db.Customers.Find(id));
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }


        }

        [HttpPost]
        public ActionResult UserEdit(int id, FormCollection frm)
        {

            Customer customer = db.Customers.Where(x => x.CustomerID == id).FirstOrDefault();
            customer.FirstName = frm["FirstName"];
            customer.LastName = frm["LastName"];
            customer.Password = frm["Password"];
            customer.Age = int.Parse(frm["Age"]);
            customer.Address1 = frm["Address"];
            customer.Mobile1 = frm["Mobile1"];
            customer.BirthDate = DateTime.Parse(frm["BirthDate"]);
            //customer.Email = frm["Email"];           UpdateProfile'a ekle..
            //customer.Country = frm["Country"];
            //customer.State = frm["State"];
            //customer.City = frm["City"];

            db.SaveChanges();

            if (AdminCheck() == true)
            {
                return RedirectToAction("UserUpdatedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }

        }

        public ActionResult UserDelete(int id)
        {
            Customer customer = db.Customers.Where(x => x.CustomerID == id).FirstOrDefault();
            db.Customers.Remove(customer);
            db.SaveChanges();

            if (AdminCheck() == true)
            {
                return RedirectToAction("UserDeletedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }
#endregion

        #region Admins
        public ActionResult ListAdmins()
        {
            TempData["AdminList"] = db.AdminLogins.ToList();

            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }

        public ActionResult EditAdmin()
        {
            TempData["EditAdmin"] = db.AdminLogins.ToList();


            if (AdminCheck() == true)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }

        public ActionResult AdminEdit(int id)
        {

            AdminLogin admin = db.AdminLogins.Where(x => x.LoginID == id).FirstOrDefault();

            if (AdminCheck() == true)
            {
                return View(db.AdminLogins.Find(id));
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }

        [HttpPost]
        public ActionResult AdminEdit(int id, FormCollection frm)
        {

            AdminLogin admin = db.AdminLogins.Where(x => x.LoginID == id).FirstOrDefault();

            admin.UserName = frm["UserName"];
            admin.AdminEmployee.FirstName = frm["AdminEmployee.FirstName"];
            admin.AdminEmployee.LastName = frm["AdminEmployee.LastName"];
            admin.Password = frm["Password"];
            admin.AdminEmployee.Address = frm["AdminEmployee.Address"];
            admin.AdminEmployee.Phone = frm["AdminEmployee.Phone"];
            admin.AdminEmployee.BirthDate = DateTime.Parse(frm["AdminEmployee.BirthDate"]);
            admin.AdminEmployee.Email = frm["AdminEmployee.Email"];


            db.SaveChanges();
            if (AdminCheck() == true)
            {
                return RedirectToAction("AdminUpdatedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }

        }

        public ActionResult AddAdmin()
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
        public ActionResult AddAdmin(FormCollection frm)
        {
            AdminLogin admin = new AdminLogin();
            AdminEmployee adminEmp = new AdminEmployee();
            admin.UserName = frm["UserName"];
            adminEmp.FirstName = frm["FirstName"];
            adminEmp.LastName = frm["LastName"];
            admin.Password = frm["Password"];
            adminEmp.Address = frm["Address"];
            adminEmp.Phone = frm["Phone"];
            adminEmp.BirthDate = DateTime.Parse(frm["BirthDate"]);
            adminEmp.Age = int.Parse(frm["Age"]);
            adminEmp.Gender = frm["Gender"] == "on" ? true : false;
            adminEmp.Email = frm["Email"];
            admin.RoleType = 1;
            admin.EmpID = adminEmp.EmpID;

            db.AdminEmployees.Add(adminEmp);
            db.AdminLogins.Add(admin);
            db.SaveChanges();

            if (AdminCheck() == true)
            {
                return RedirectToAction("AdminAddedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }

        public ActionResult AdminDelete(int id)
        {
            AdminLogin admin = db.AdminLogins.Where(x => x.LoginID == id).FirstOrDefault();

            db.AdminLogins.Remove(admin);
            db.SaveChanges();

            if (AdminCheck() == true)
            {
                return RedirectToAction("AdminDeletedSuccessfully");
            }
            else
            {
                return RedirectToAction("Login", "AdminLogin");
            }
        }
#endregion

        #region SuccessPages
        public ActionResult UserUpdatedSuccessfully()
        {
            return View();
        }

        public ActionResult UserDeletedSuccessfully()
        {
            return View();
        }

        public ActionResult AdminUpdatedSuccessfully()
        {
            return View();
        }

        public ActionResult AdminAddedSuccessfully()
        {
            return View();
        }

        public ActionResult AdminDeletedSuccessfully()
        {
            return View();
        }
        #endregion
    }
}