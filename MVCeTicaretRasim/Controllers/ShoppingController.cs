using MVCeTicaretRasim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCeTicaretRasim.Controllers
{
    public class ShoppingController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult AddToCart(int id)
        {
            if (Session["OnlineKullanici"] == null)
            {
                return RedirectToAction("Login", "Login");
            }



            OrderDetail od = db.OrderDetails.Where(x => x.ProductID == id && x.IsCompleted == false).FirstOrDefault();

            if (od == null)
            {
                od = new OrderDetail();
                od.ProductID = id;
                od.CustomerID = TemporaryUserData.OnlineUserID;
                od.UnitPrice = db.Products.Find(id).UnitPrice;
                od.Quantity = 1;
                od.Discount = db.Products.Find(id).Discount;
                od.TotalAmount = od.Quantity * od.UnitPrice * (1 - od.Discount);
                od.OrderDate = new DateTime(0001, 1, 1, 1, 1, 1);
                od.IsCompleted = false;

                db.OrderDetails.Add(od);
            }
            else
            {
                od.Quantity++;
                od.TotalAmount = od.Quantity * od.UnitPrice * (1 - od.Discount);
            }

            db.SaveChanges();

            return RedirectToAction("ProductDetail", "Products", new { id = id });
        }

        public ActionResult AddToWishlist(int id)
        {
            if (Session["OnlineKullanici"] == null)
            {
                return RedirectToAction("Login", "Login");
            }

            Wishlist wishlist = db.Whislists.Where(x => x.ProductID == id).FirstOrDefault();

            if (wishlist == null)
            {
                wishlist = new Wishlist();
                wishlist.CustomerID = TemporaryUserData.OnlineUserID;
                wishlist.ProductID = id;
                wishlist.IsActive = true;

                db.Whislists.Add(wishlist);
                db.SaveChanges();
            }



            return RedirectToAction("ProductDetail", "Products", new { id = id });
        }

        public ActionResult Cart()
        {
            TempData["OrderDetails"] = db.OrderDetails.Where(x => x.CustomerID == TemporaryUserData.OnlineUserID && x.IsCompleted == false).ToList();

            return View();
        }

        public ActionResult Wishlist()
        {
            TempData["Wishlist"] = db.Whislists.Where(x => x.CustomerID == TemporaryUserData.OnlineUserID).ToList();

            return View();
        }

        public ActionResult RemoveFromCart(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Where(x => x.ProductID == id).FirstOrDefault();

            db.OrderDetails.Remove(orderDetail);
            db.SaveChanges();

            return RedirectToAction("Cart");
        }

        public ActionResult AddToWishlistFromCart(int id)
        {
            OrderDetail orderDetail = db.OrderDetails.Where(x => x.ProductID == id).FirstOrDefault();
            db.OrderDetails.Remove(orderDetail);

            Wishlist wishlist = db.Whislists.Where(x => x.ProductID == id).FirstOrDefault();

            if (wishlist == null)
            {
                wishlist = new Wishlist();
                wishlist.CustomerID = TemporaryUserData.OnlineUserID;
                wishlist.ProductID = id;
                wishlist.IsActive = true;

                db.Whislists.Add(wishlist);
            }
            db.SaveChanges();

            return RedirectToAction("Cart");
        }

        public ActionResult AddToCartFromWishlist(int id)
        {
            Wishlist wishlist = db.Whislists.Where(x => x.ProductID == id).FirstOrDefault();
            db.Whislists.Remove(wishlist);

            OrderDetail od = db.OrderDetails.Where(x => x.ProductID == id).FirstOrDefault();

            if (od == null)
            {
                od = new OrderDetail();
                od.ProductID = id;
                od.CustomerID = TemporaryUserData.OnlineUserID;
                od.UnitPrice = db.Products.Find(id).UnitPrice;
                od.Quantity = 1; // product detail icindeki mıktar kısmından cekilebilir:::
                od.Discount = db.Products.Find(id).Discount;
                od.TotalAmount = od.Quantity * od.UnitPrice * (1 - od.Discount);
                od.OrderDate = DateTime.Now;
                od.IsCompleted = false;

                db.OrderDetails.Add(od);
            }



            db.SaveChanges();

            return RedirectToAction("Wishlist");
        }

        public ActionResult RemoveFromWishlist(int id)
        {
            Wishlist wishlist = db.Whislists.Where(x => x.ProductID == id).FirstOrDefault();

            db.Whislists.Remove(wishlist);
            db.SaveChanges();

            return RedirectToAction("Wishlist");
        }

        [HttpPost]
        public ActionResult UpdateQuantity(int id, FormCollection frm)
        {
            OrderDetail orderDetail = db.OrderDetails.Where(x => x.ProductID == id && x.IsCompleted == false).FirstOrDefault();

            orderDetail.Quantity = int.Parse(frm["Quantity"]);
            orderDetail.TotalAmount = int.Parse(frm["Quantity"]) * orderDetail.UnitPrice * (1 - orderDetail.Discount);

            db.SaveChanges();
            return RedirectToAction("Cart");
        }

        public ActionResult ContinueShopping()
        {
            TempData["OrderDetails"] = db.OrderDetails.Where(x => x.CustomerID == TemporaryUserData.OnlineUserID && x.IsCompleted == false).ToList();

            return View();
        }

        public ActionResult OpenPaymentsPage()
        {
            TempData["PaymentTypes"] = db.PaymentTypes.ToList();

            return View(db.Customers.FirstOrDefault(x => x.CustomerID == TemporaryUserData.OnlineUserID));
        }
        [HttpPost]
        public ActionResult CompleteShopping(FormCollection frm)
        {
            Customer customer = db.Customers.FirstOrDefault(x => x.CustomerID == TemporaryUserData.OnlineUserID);
            customer.FirstName = frm["FirstName"];
            customer.LastName = frm["LastName"];
            customer.Email = frm["Email"];
            customer.Mobile1 = frm["Mobile1"];
            customer.Address1 = frm["Address1"];
            customer.City = frm["City"];
            customer.Country = frm["Country"];


            Payment payment = new Payment();
            payment.Type = int.Parse(frm["PaymentType"]);
            payment.PaymentDateTime = DateTime.Now;
            db.Payments.Add(payment);

            ShippingDetail shippingDetail = new ShippingDetail();
            shippingDetail.FirstName = customer.FirstName;
            shippingDetail.LastName = customer.LastName;
            shippingDetail.Email = customer.Email;
            shippingDetail.Mobile = customer.Mobile1;
            shippingDetail.Address = customer.Address1;
            shippingDetail.City = customer.City;
            db.ShippingDetails.Add(shippingDetail);


            List<OrderDetail> orderDetails = db.OrderDetails.Where(x => x.CustomerID == TemporaryUserData.OnlineUserID && x.IsCompleted == false).ToList();
            foreach (OrderDetail item in orderDetails)
            {
                Order order = new Order();
                order.OrderDetailID = item.OrderDetailID;
                order.PaymentID = payment.PaymentID;
                order.ShippingID = shippingDetail.ShippingID;
                order.Discount = item.Discount;
                order.Taxes = 0.18m;
                order.TotalAmount = item.TotalAmount;
                order.IsCompleted = true;
                order.OrderDate = DateTime.Now;
                order.Dispatched = true;
                order.DispatchedDate = DateTime.Now;
                order.Shipped = false;
                order.ShippedDate = DateTime.Now.AddDays(2);
                order.Delivered = false;
                order.DeliveryDate = DateTime.Now.AddDays(7);
                order.CancelOrder = false;


                db.Orders.Add(order);

                item.IsCompleted = true;

                Product product = db.Products.FirstOrDefault(x => x.ProductID == item.ProductID);
                product.UnitsInStock -= item.Quantity;
                product.UnitOnOrder += item.Quantity;
            }

            db.SaveChanges();
            return View();
        }
    }
}