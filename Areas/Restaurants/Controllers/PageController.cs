using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Models;
using Simple.UserModels;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using Microsoft.Owin.Security;
using Simple.Identity;
using System.Data.Entity;

namespace Simple.Areas.Restaurants.Controllers
{
    [Authorize(Roles = "Restaurant")]
    public class PageController : Controller
    {
        private ApplicationDb db = new ApplicationDb();
        private AppDbContext appDbContext = new AppDbContext();
        // GET: Restaurants/Page
        public ActionResult Edit()
        {
            
            var appUserStore = new AppUserStore(appDbContext);
            var appUserManager = new AppUserManager(appUserStore);
            string userId = User.Identity.GetUserId();
            var rtrID = appUserManager.FindById(userId).RtrID;
            
            var app = appDbContext.Users.Find(userId).RtrID;
            ViewBag.waiting = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == null);
            ViewBag.received = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == "Đồng ý nhận đơn");
            Restaurant restaurant = db.Restaurants.Find(rtrID);
            ViewBag.inactive = restaurant.Active;
            ViewBag.CityList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Hà Nội", Text = "Hà Nội" },
            new SelectListItem { Value = "Hải Phòng", Text = "Hải Phòng" },
            new SelectListItem { Value = "Nam Định", Text = "Nam Định" }
        };
            return View(restaurant);
        }
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RtrID,RtrRealName,RtrEmail,RtrPhone,RtrCity,RtrAddress,RtrAvatar,RtrDesc,OpenTime,CloseTime,MinPrice,MaxPrice")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            string userId = User.Identity.GetUserId();
            var app = appDbContext.Users.Find(userId).RtrID;
            ViewBag.waiting = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == null);
            ViewBag.received = db.Orders.Count(
                u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                u.Happen == false && u.RtrAction == "Đồng ý nhận đơn");
            ViewBag.CityList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Hà Nội", Text = "Hà Nội" },
            new SelectListItem { Value = "Hải Phòng", Text = "Hải Phòng" },
            new SelectListItem { Value = "Nam Định", Text = "Nam Định" }
        };
            return View(restaurant);
        }
        
    }
}