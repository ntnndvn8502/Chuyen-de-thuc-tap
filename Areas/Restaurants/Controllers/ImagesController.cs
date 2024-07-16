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
using Simple.Identity;

namespace Simple.Areas.Restaurants.Controllers
{
    [Authorize (Roles = "Restaurant")]
    public class ImagesController : Controller
    {
        private ApplicationDb db = new ApplicationDb();
        private AppDbContext appDbContext = new AppDbContext();
        // GET: Restaurants/Images
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
            var images = db.Images.Where(u => u.RtrID == app);
            return View(images.ToList());
            
        }

        // GET: Restaurants/Images/Details/5

        // GET: Restaurants/Images/Create

        // POST: Restaurants/Images/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Index")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ImageID,ImageUrl")] Image image)
        {
            if (image.ImageUrl != null)
            {
                string userId = User.Identity.GetUserId();
                var app = appDbContext.Users.Find(userId).RtrID;
                image.RtrID = (int)app;
                db.Images.Add(image);
                db.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                string userId = User.Identity.GetUserId();
                var app = appDbContext.Users.Find(userId).RtrID;
                ViewBag.waiting = db.Orders.Count(
                    u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                    u.Happen == false && u.RtrAction == null);
                ViewBag.received = db.Orders.Count(
                    u => u.RtrID == app && u.UserAction == "Đã đặt đơn" &&
                    u.Happen == false && u.RtrAction == "Đồng ý nhận đơn");
                return View(image);
            }
            
        }

        // GET: Restaurants/Images/Edit/5
        

        // POST: Restaurants/Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageID,ImageUrl")] Image image)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                var app = appDbContext.Users.Find(userId).RtrID;
                image.RtrID = (int)app;
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return RedirectToAction("Index");
        }

        // GET: Restaurants/Images/Delete/5
        

        // POST: Restaurants/Images/Delete/5
        public ActionResult Delete(int id)
        {
            Image image = db.Images.Find(id);
            db.Images.Remove(image);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
