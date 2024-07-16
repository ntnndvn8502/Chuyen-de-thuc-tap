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

namespace Simple.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register vm)
        {
            if (ModelState.IsValid)
            {
                var db = new ApplicationDb();
                var appDbContext = new AppDbContext();
                var appUserStore = new AppUserStore(appDbContext);
                var appUserManager = new AppUserManager(appUserStore);
                var passwordHash = Crypto.HashPassword(vm.Password);
                if (appUserManager.FindByName(vm.Username) != null)
                {
                    ModelState.AddModelError("Lỗi", "Tên đăng nhập đã tồn tại");
                    ViewBag.error = "Tên đăng nhập đã tồn tại";
                    return View();
                } else
                {
                    var user = new AppUser()
                    {
                        UserName = vm.Username,
                        RealName = vm.RealName,
                        Email = vm.Email,
                        Phone = vm.Phone,
                        PasswordHash = passwordHash
                    };
                    

                    IdentityResult identityResult = appUserManager.Create(user);
                    if (identityResult.Succeeded)
                    {
                        appUserManager.AddToRole(user.Id, "user");
                        var authenManager = HttpContext.GetOwinContext().Authentication;
                        var userIdentity = appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                        authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                        Customer customer = new Customer()
                        {
                            CustomerID = user.Id,
                            CustomerUserName = user.UserName,
                            CustomerRealName = user.RealName,
                            CustomerEmail = vm.Email,
                            CustomerPhone = vm.Phone
                        };
                        db.Customers.Add(customer);
                        db.SaveChanges();
                    }
                    TempData["message"] = "Đăng kí thành công";
                    return RedirectToAction("Notification", "Home");
                }
               
            } else
            {
                ModelState.AddModelError("Lỗi", "Dữ liệu không hợp lệ");
                return View();
            }
            
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login vm)
        {
            var appDbContext = new AppDbContext();
            var appUserStore = new AppUserStore(appDbContext);
            var appUserManager = new AppUserManager(appUserStore);
            var user = appUserManager.Find(vm.Username, vm.Password);
            
            if(user != null )
            {
                var authenManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = appUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenManager.SignIn(new AuthenticationProperties(), userIdentity);
                if(appUserManager.IsInRole(user.Id, "Admin"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                } else
                if (appUserManager.IsInRole(user.Id, "Restaurant"))
                {
                    return RedirectToAction("Index", "Home", new { area = "Restaurants" });
                }
                
                return RedirectToAction("Index", "Home");

            } else
            {
                ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View();
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            
            var authenManager = HttpContext.GetOwinContext().Authentication;
            authenManager.SignOut();
            
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Edit()
        {
            var appDbContext = new AppDbContext();
            var appUserStore = new AppUserStore(appDbContext);
            var appUserManager = new AppUserManager(appUserStore);
            string userId = User.Identity.GetUserId();
            AppUser app = appUserManager.FindById(userId);
            Edit edit = new Edit()
            {
                Id = app.Id,
                Email = app.Email,
                Phone = app.Phone,
                RealName = app.RealName
            };
            return View(edit);
        }
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Email,Phone")] AppUser user)
        {
            var db = new ApplicationDb();
            var appDbContext = new AppDbContext();
            var appUserStore = new AppUserStore(appDbContext);
            var appUserManager = new AppUserManager(appUserStore);
            string userId = User.Identity.GetUserId();
            AppUser app = appUserManager.FindById(userId);
            app.Email = user.Email;
            app.Phone = user.Phone;
            appUserManager.Update(app);
            Customer customer = new Customer()
            {
                CustomerID = userId,
                CustomerUserName = app.UserName,
                CustomerEmail = user.Email,
                CustomerRealName = app.RealName,
                CustomerPhone = user.Phone
            };
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
            Edit edit = new Edit()
            {
                Id = app.Id,
                Email = app.Email,
                Phone = app.Phone,
                RealName = app.RealName
            };
            return View(edit);
        }


    }
}