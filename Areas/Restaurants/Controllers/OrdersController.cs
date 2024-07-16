using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Simple.Identity;
using Simple.Models;

namespace Simple.Areas.Restaurants.Controllers
{
    [Authorize(Roles = "Restaurant")]
    public class OrdersController : Controller
    {
        // GET: Restaurants/Orders
        private ApplicationDb db = new ApplicationDb();
        private AppDbContext appDbContext = new AppDbContext();
        /*var appUserStore = new AppUserStore(appDbContext);
        var appUserManager = new AppUserManager(appUserStore);*/
        

        public ActionResult History(int? id)
        {
            
            ViewBag.id = id;
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;
            Restaurant restaurant = db.Restaurants.Find(app);
            ViewBag.inactive = restaurant.Active;

            ViewBag.waiting = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == null);
            ViewBag.received = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == "Đồng ý nhận đơn");
            var orders = db.Orders.Where(
                u => u.RtrID == app && (u.Happen == true || 
                u.Happen == false &&( u.UserAction != "Đã đặt đơn" ||
                u.RtrAction == "Từ chối nhận đơn" )));
            if (id == 1)
            {
                var list = orders.Where(i => i.Happen == true).ToList();
                return View(list);
                
            } else if (id == 2)
            {
                var list = orders.Where(i => i.Happen == false).ToList();
                return View(list);
            } else
            {
                return View(orders.ToList());
            }

            
        }
        public ActionResult Waiting()
        {
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;
            Restaurant restaurant = db.Restaurants.Find(app);
            ViewBag.inactive = restaurant.Active;

            ViewBag.waiting = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == null);
            ViewBag.received = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == "Đồng ý nhận đơn");
            var orders = db.Orders.Where(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == null).ToList();
            return View(orders);
        }
        public ActionResult Received()
        {
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;
            Restaurant restaurant = db.Restaurants.Find(app);
            ViewBag.inactive = restaurant.Active;

            ViewBag.waiting = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == null);
            ViewBag.received = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == "Đồng ý nhận đơn");
            var orders = db.Orders.Where(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == "Đồng ý nhận đơn").ToList();
            return View(orders);
        }
        public ActionResult Review()
        {
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;
            Restaurant restaurant = db.Restaurants.Find(app);
            ViewBag.inactive = restaurant.Active;

            ViewBag.waiting = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == null);
            ViewBag.received = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == "Đồng ý nhận đơn");
            List<Order> reviews = db.Orders.Where(u => u.RtrID == app && u.Rate > 0).ToList();
            return View(reviews);
        }
        public ActionResult Served(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            if (order.RtrID == app && order.Happen == false && order.UserAction == "Đã đặt đơn" && order.RtrAction == "Đồng ý nhận đơn")
            {
                order.Happen = true;
                db.SaveChanges();

            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Received");
        }
        public ActionResult Absent(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            if (order.RtrID == app && order.Happen == false && order.UserAction == "Đã đặt đơn" && order.RtrAction == "Đồng ý nhận đơn")
            {
                order.UserAction = "Khách hàng không đến";
                db.SaveChanges();

            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Received");
        }

        public ActionResult Accept(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            if (order.RtrID == app && order.Happen == false && order.UserAction == "Đã đặt đơn" && order.RtrAction == null)
            {
                order.RtrAction = "Đồng ý nhận đơn";
                db.SaveChanges();

            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Waiting");
        }
        public ActionResult Refuse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            if (order.RtrID == app && order.Happen == false && order.UserAction == "Đã đặt đơn" && order.RtrAction != "Từ chối nhận đơn")
            {
                order.RtrAction = "Từ chối nhận đơn";
                db.SaveChanges();

            }
            else
            {
                return HttpNotFound();
            }
            string previousUrl = Request.UrlReferrer?.ToString();
            return Redirect(previousUrl) ;
        }
    }
}