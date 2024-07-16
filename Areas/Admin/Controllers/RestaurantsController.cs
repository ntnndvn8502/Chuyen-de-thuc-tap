using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Simple.Models;
using Simple.Identity;
using Simple.ViewModel;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;

namespace Simple.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RestaurantsController : Controller
    {
        private ApplicationDb db = new ApplicationDb();
        private AppDbContext appDbContext = new AppDbContext();

        // GET: Admin/Restaurants
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Restaurant);
            double getStars(int id)
            {
                try
                {
                    double result = orders.Where(u => u.RtrID == id && u.Rate > 0).Average(i => i.Rate);
                    return Math.Round(result, 1);
                }
                catch
                {
                    double result = 0;
                    return result;
                }

            }
            var restaurants = db.Restaurants.ToList();

            List<HomeViewModel> homeViewModels = restaurants.Select(rtr => new HomeViewModel()
            {
                RtrID = rtr.RtrID,
                
                RtrRealName = rtr.RtrRealName,
                RtrAddress = rtr.RtrAddress,
                RtrCity = rtr.RtrCity,
                UserName = appDbContext.Users.Where(u => u.RtrID == rtr.RtrID).FirstOrDefault().UserName,
                Stars = getStars(rtr.RtrID)

            }).ToList();
            return View(homeViewModels);
            

        }

        // GET: Admin/Restaurants/Details/5
        public ActionResult Details(int? id)
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
            List<AppUser> appUsers = appDbContext.Users.Where(u => u.RtrID == id).ToList();
            ViewBag.Account = appUsers;
            return View(restaurant);
            
        }

        // GET: Admin/Restaurants/Create
        public ActionResult Create()
        {
            ViewBag.CityList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Hà Nội", Text = "Hà Nội" },
            new SelectListItem { Value = "Hải Phòng", Text = "Hải Phòng" },
            new SelectListItem { Value = "Nam Định", Text = "Nam Định" }
        };
            return View();
        }

        // POST: Admin/Restaurants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RtrID,RtrRealName,RtrCity,RtrAddress")] Restaurant restaurant, string Username, string Password)
        {
            if (ModelState.IsValid)
            {
                var appUserStore = new AppUserStore(appDbContext);
                var appUserManager = new AppUserManager(appUserStore);
                var passwordHash = Crypto.HashPassword(Password);
                
                
                if (appUserManager.FindByName(Username) != null)
                {
                    ViewBag.CityList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Hà Nội", Text = "Hà Nội" },
            new SelectListItem { Value = "Hải Phòng", Text = "Hải Phòng" },
            new SelectListItem { Value = "Nam Định", Text = "Nam Định" }
        };
                    ViewBag.error = "Tên đăng nhập đã tồn tại";
                    return View();
                } else
                {
                    db.Restaurants.Add(restaurant);
                    db.SaveChanges();
                    var user = new AppUser()
                    {
                        UserName = Username,
                        RtrID = restaurant.RtrID,
                        PhoneNumber = Password,
                        PasswordHash = passwordHash
                    };

                    IdentityResult identityResult = appUserManager.Create(user);
                    if (identityResult.Succeeded)
                    {
                        
                        appUserManager.AddToRole(user.Id, "Restaurant");
                        return RedirectToAction("Index");

                    }
                }
                
            }
            ViewBag.CityList = new List<SelectListItem>
        {
            new SelectListItem { Value = "Hà Nội", Text = "Hà Nội" },
            new SelectListItem { Value = "Hải Phòng", Text = "Hải Phòng" },
            new SelectListItem { Value = "Nam Định", Text = "Nam Định" }
        };
            return View(restaurant);
        }

        // GET: Admin/Restaurants/Edit/5
        public ActionResult Edit(int? id)
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
            return View(restaurant);
        }

        // POST: Admin/Restaurants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RtrID,RtrRealName,RtrEmail,RtrPhone,RtrCity,RtrAddress,RtrAvatar,RtrDesc,OpenTime,CloseTime,RtrStatus")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Admin/Restaurants/Delete/5
        public ActionResult Delete(int? id)
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
            return View(restaurant);
        }

        // POST: Admin/Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            var appUsers = appDbContext.Users.Where(u => u.RtrID == id).ToList();
            foreach (var appUser in appUsers)
            {
                appDbContext.Users.Remove(appUser);
                
            }
            appDbContext.SaveChanges();
            var orders = db.Orders.Where(u => u.RtrID == id).ToList();
            db.Orders.RemoveRange(orders);
            db.Restaurants.Remove(restaurant);
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
