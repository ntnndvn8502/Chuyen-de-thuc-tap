using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Models;
using Simple.Identity;
using Microsoft.AspNet.Identity;
using System.Net;

namespace Simple.Areas.Restaurants.Controllers
{
    [Authorize(Roles = "Restaurant")]
    public class HomeController : Controller
    {
        // GET: Restaurants/Home
        private ApplicationDb db = new ApplicationDb();
        private AppDbContext appDbContext = new AppDbContext();
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;
           
            ViewBag.waiting = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == null);
            ViewBag.received = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == "Đồng ý nhận đơn");
            Restaurant restaurant = db.Restaurants.Find(app);
            ViewBag.inactive = restaurant.Active;
            ViewBag.Name = restaurant.RtrRealName;
            ViewBag.Address = restaurant.RtrAddress;
            ViewBag.City = restaurant.RtrCity;
            return View();
        }
        public ActionResult InActive(bool? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;
            Restaurant restaurant = db.Restaurants.Find(app);
            restaurant.Active = (bool)id;
            db.SaveChanges();
            string referrerUrl = Request.UrlReferrer != null ? Request.UrlReferrer.ToString() : "";

            // Chuyển hướng về trang trước đó
            return Redirect(referrerUrl);


        }
    }
}