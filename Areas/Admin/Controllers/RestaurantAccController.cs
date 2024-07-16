using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Models;
using Simple.RestaurantModels;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using Microsoft.Owin.Security;
using Simple.Identity;
using System.Data.Entity;
using System.Net;

namespace Simple.Areas.Admin.Controllers
{
    
    [Authorize(Roles = "Admin")]
    public class RestaurantAccController : Controller
    {
        private ApplicationDb db = new ApplicationDb();
        private AppDbContext appDbContext = new AppDbContext();

        // GET: Admin/RestaurantAcc
        public ActionResult Index()
        {
            var db = new ApplicationDb();
            var users = appDbContext.Users.Where(u => u.Roles.Any(r => r.RoleId == "0d1d05aa-fc0b-41af-ae6f-6d24f82e4e1c")).ToList();
            var restaurants = db.Set<Restaurant>().ToList();
            var usersWithRestaurantInfo = users
             .Join(restaurants,
                 user => user.RtrID, // Khóa ngoại của AppUser
                 restaurant => restaurant.RtrID, // Khóa chính của Restaurant
                 (user, restaurant) =>
                 {
                     user.Restaurant = restaurant; // Gán thông tin nhà hàng cho AppUser
                    return user;
                 })
             .ToList();
            return View(usersWithRestaurantInfo);
        }
        /*public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser user = appDbContext.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]*/
        public ActionResult Delete(string id)
        {
            AppUser user = appDbContext.Users.Find(id);
            appDbContext.Users.Remove(user);
            appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Register(int? id)
        {
            if (id != null)
            {
                ViewBag.RtrID = id;
            }
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register vm)
        {
            if (ModelState.IsValid)
            {

                
                var appUserStore = new AppUserStore(appDbContext);
                var appUserManager = new AppUserManager(appUserStore);
                var passwordHash = Crypto.HashPassword(vm.Password);
                if (appUserManager.FindByName(vm.Username) != null)
                {
                    ModelState.AddModelError("Lỗi", "Tên đăng nhập đã tồn tại");
                    ViewBag.error = "Tên đăng nhập đã tồn tại";
                    return View();
                }
                if(db.Restaurants.Find(vm.RtrID) == null)
                {
                    ViewBag.error2 = "Không tồn tại mã nhà hàng này";
                    return View();
                }
                else
                {
                    var user = new AppUser()
                    {
                        UserName = vm.Username,
                        RtrID = vm.RtrID,
                        SecurityStamp = vm.Password,
                        PasswordHash = passwordHash
                    };

                    IdentityResult identityResult = appUserManager.Create(user);
                    if (identityResult.Succeeded)
                    {
                        appUserManager.AddToRole(user.Id, "Restaurant");
                        
                    }
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ModelState.AddModelError("Lỗi", "Dữ liệu không hợp lệ");
                return View();
            }

        }
    }
}