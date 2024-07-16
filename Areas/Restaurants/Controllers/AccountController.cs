using Microsoft.AspNet.Identity;
using Simple.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simple.Areas.Restaurants.Controllers
{
    public class AccountController : Controller
    {
        // GET: Restaurants/Account
        public ActionResult Edit()
        {
            var appDbContext = new AppDbContext();
            var appUserStore = new AppUserStore(appDbContext);
            var appUserManager = new AppUserManager(appUserStore);
            string userId = User.Identity.GetUserId();
            AppUser app = appUserManager.FindById(userId);
            return View(app);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Email,Phone")] AppUser user)
        {
            var appDbContext = new AppDbContext();
            var appUserStore = new AppUserStore(appDbContext);
            var appUserManager = new AppUserManager(appUserStore);
            string userId = User.Identity.GetUserId();
            AppUser app = appUserManager.FindById(userId);
            app.Email = user.Email;
            app.Phone = user.Phone;
            appUserManager.Update(app);
            return View();
        }
    }
}