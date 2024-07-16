using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Identity;
using Microsoft.AspNet.Identity;
using System.Net;
using Simple.Models;

namespace Simple.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        // GET: Admin/User
        private AppDbContext appDbContext = new AppDbContext();
        private ApplicationDb db = new ApplicationDb();

        public ActionResult Index()
        {
            
            List<AppUser> users = appDbContext.Users.Where(u => u.Roles.Any(r => r.RoleId == "72ebdec7-708a-41ce-9fa8-6a64ebe403ea")).ToList();
            return View(users);
        }
        
        public ActionResult Delete(string id)
        {
            AppUser user = appDbContext.Users.Find(id);
            appDbContext.Users.Remove(user);
            appDbContext.SaveChanges();
            var orders = db.Orders.Where(u => u.CustomerId == id).ToList();
            db.Orders.RemoveRange(orders);
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Restaurants/Delete/5
       

    }
}