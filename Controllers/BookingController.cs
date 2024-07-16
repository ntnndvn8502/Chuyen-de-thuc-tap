using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Simple.Models;

namespace Simple.Controllers
{
    [Authorize(Roles = "User")]
    public class BookingController : Controller
    {
        private ApplicationDb db = new ApplicationDb();

        // GET: Booking
        
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            if (restaurant.Active)
            {
                TempData["message"] = "Nhà hàng đang tạm ngừng nhận đơn";
                return RedirectToAction("Notification", "Home");

            }
            string userId = User.Identity.GetUserId();
            int count = db.Orders.Count(u =>
            u.CustomerId == userId && u.Happen == false && u.UserAction == "Đã đặt đơn" && u.RtrAction != "Từ chối nhận đơn");
            ViewBag.Rtr = restaurant;
            /*var userID = User.Identity.GetUserId();*/
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RtrID,OrderID,ArrivalDate,ArrivalTime,Quantity,OrderNote")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.CustomerId = User.Identity.GetUserId();
                
                order.UserAction = "Đã đặt đơn";
                order.BookTime = DateTime.Now;
                db.Orders.Add(order);
                db.SaveChanges();
                TempData["message"] = "Tạo đơn thành công";
                return RedirectToAction("Notification", "Home");
            }
            string userId = User.Identity.GetUserId();
            int count = db.Orders.Count(u =>
            u.CustomerId == userId && u.Happen == false && u.UserAction == "Đã đặt đơn" && u.RtrAction != "Từ chối nhận đơn");

            return View();
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            ViewBag.Rtr = db.Restaurants.Find(order.RtrID);
            if (order == null || order.Happen == true ||
                !(order.UserAction == "Đã đặt đơn" && 
                (order.RtrAction == "Đồng ý nhận đơn" || order.RtrAction == null)))
            {
                return HttpNotFound();
            }
            string userId = User.Identity.GetUserId();
            int count = db.Orders.Count(u =>
            u.CustomerId == userId && u.Happen == false && u.UserAction == "Đã đặt đơn" && u.RtrAction != "Từ chối nhận đơn");
            return View(order);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="OrderID, RtrID,CustomerID,ArrivalDate,ArrivalTime,Quantity,OrderNote")] Order order)
        {
            if (ModelState.IsValid)
            {
                order.BookTime = DateTime.Now;
                order.UserAction = "Đã đặt đơn";
                order.RtrAction = null;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = "Chỉnh sửa đơn hàng thành công";
                return RedirectToAction("Notification", "Home");
            }
            ViewBag.Rtr = db.Restaurants.Find(order.RtrID);
            string userId = User.Identity.GetUserId();
            int count = db.Orders.Count(u =>
            u.CustomerId == userId && u.Happen == false && u.UserAction == "Đã đặt đơn" && u.RtrAction != "Từ chối nhận đơn");
            return View(order);
            
        }
        public ActionResult History()
        {
            string userId = User.Identity.GetUserId();
            int count = db.Orders.Count(u =>
            u.CustomerId == userId && u.Happen == false && u.UserAction == "Đã đặt đơn" && u.RtrAction != "Từ chối nhận đơn");
            ViewBag.Count = count;
            var orders = db.Orders.Where(
                u => u.CustomerId == userId && ( u.Happen == true ||
                u.Happen == false && (u.UserAction == "Hủy bởi khách hàng" ||
                u.UserAction == "Khách hàng không đến" || u.RtrAction == "Từ chối nhận đơn" ))).ToList();
            return View(orders);
        }
        public ActionResult Rate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var order = db.Orders.Include(u => u.Restaurant).FirstOrDefault(u => u.OrderID == id);
            if (order == null || order.Happen == false)
            {
                return HttpNotFound();
            }
            string userId = User.Identity.GetUserId();
            int count = db.Orders.Count(u =>
            u.CustomerId == userId && u.Happen == false && u.UserAction == "Đã đặt đơn" && u.RtrAction != "Từ chối nhận đơn");

            return View(order);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rate(int? id, int Rate, string Comment)
        {
            
            Order order = db.Orders.Find(id);
            order.Rate = Rate;
            order.Comment = Comment;
            db.SaveChanges();
            return RedirectToAction("History");
        }
        public ActionResult Waiting()
        {
            string userId = User.Identity.GetUserId();
            var orders = db.Orders.Where(
                u => u.CustomerId == userId && u.Happen == false &&
                u.UserAction == "Đã đặt đơn" && u.RtrAction != "Từ chối nhận đơn");
            
            int count = db.Orders.Count(u =>
            u.CustomerId == userId && u.Happen == false && u.UserAction == "Đã đặt đơn" && u.RtrAction != "Từ chối nhận đơn");

            return View(orders.ToList());

        }
        
        public ActionResult Cancel(int? id)
        {
            
            
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
            string userId = User.Identity.GetUserId();

            Order order = db.Orders.Find(id);
            if (order == null)
                {
                    return HttpNotFound();
                }
            if (order.CustomerId == userId && order.Happen == false && order.UserAction == "Đã đặt đơn" && order.RtrAction != "Từ chối nhận đơn")
            {
                order.UserAction = "Hủy bởi khách hàng";
                db.SaveChanges();
                
            } else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Waiting");
            

        }


        // GET: Booking/Details/5
      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
