using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using Simple.Models;
using Simple.ViewModel;
using Microsoft.AspNet.Identity;

namespace Simple.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDb db = new ApplicationDb();
        
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Restaurant);
            double getStars(int id)
            {
                try
                {
                    double result = orders.Where(u => u.RtrID == id && u.Rate > 0).Average(i => i.Rate);
                    return Math.Round(result,1);
                } catch
                {
                    double result = 0;
                    return result;
                }
                
            }
            var restaurants = db.Restaurants.ToList();
            
            List<HomeViewModel> homeViewModels = restaurants.Select(rtr => new HomeViewModel()
            {
                RtrID = rtr.RtrID,
                RtrAvatar = rtr.RtrAvatar,
                RtrRealName = rtr.RtrRealName,
                RtrAddress = rtr.RtrAddress,
                RtrCity = rtr.RtrCity,
                MinPrice = rtr.MinPrice,
                MaxPrice = rtr.MaxPrice,
                InActive = rtr.Active,
                Stars = getStars(rtr.RtrID)

            }).ToList();
            if (User.Identity.IsAuthenticated)
            {
                string userId = User.Identity.GetUserId();
                int count = db.Orders.Count(u =>
                u.CustomerId == userId && u.Happen == false && u.UserAction == "Đã đặt đơn" && u.RtrAction != "Từ chối nhận đơn");
            }
            homeViewModels.Sort((a, b) => b.Stars.CompareTo(a.Stars));
            return View(homeViewModels);
        }
        public ActionResult City(string id) {
            string[] array = { "Hà Nội", "Hải Phòng", "Nam Định" };
            if (!array.Contains(id))
            {
                return HttpNotFound();
            }
            ViewBag.City = id;
            var orders = db.Orders.Include(o => o.Restaurant);
            double getStars(int x)
            {
                try
                {
                    double result = orders.Where(u => u.RtrID == x && u.Rate > 0).Average(i => i.Rate);
                    return Math.Round(result, 1);
                }
                catch
                {
                    double result = 0;
                    return result;
                }

            }
            var restaurants = db.Restaurants.Where(u =>u.RtrCity == id).ToList();

            List<HomeViewModel> homeViewModels = restaurants.Select(rtr => new HomeViewModel()
            {
                RtrID = rtr.RtrID,
                RtrAvatar = rtr.RtrAvatar,
                RtrRealName = rtr.RtrRealName,
                RtrAddress = rtr.RtrAddress,
                RtrCity = rtr.RtrCity,
                MinPrice = rtr.MinPrice,
                MaxPrice = rtr.MaxPrice,
                Stars = getStars(rtr.RtrID)

            }).ToList();

            return View(homeViewModels);

        }
        public ActionResult All()
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
                RtrAvatar = rtr.RtrAvatar,
                RtrRealName = rtr.RtrRealName,
                RtrAddress = rtr.RtrAddress,
                RtrCity = rtr.RtrCity,
                MinPrice = rtr.MinPrice,
                MaxPrice = rtr.MaxPrice,
                Stars = getStars(rtr.RtrID)

            }).ToList();
            homeViewModels.Sort((a, b) => b.Stars.CompareTo(a.Stars));
            return View(homeViewModels);
        }

        

        public ActionResult Restaurant(int? id)
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
            List<Image> images = db.Images.Where(u => u.RtrID == id).ToList();
            ViewData["Images"] = images;
            List<Order> orders = db.Orders.Include(u => u.Customer).Where(u => u.Rate > 0 && u.RtrID == id).ToList();
            
            if (orders.Any())
            {
                ViewBag.Stars = Math.Round(orders.Average(i => i.Rate), 1);
                ViewBag.Rate = orders;
            }
            
            return View(restaurant);
            
        }
        [HttpPost]
        public ActionResult Find(string search)
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

            var homeViewModels = restaurants.Select(rtr => new HomeViewModel()
            {
                RtrID = rtr.RtrID,
                RtrAvatar = rtr.RtrAvatar,
                RtrRealName = rtr.RtrRealName,
                RtrAddress = rtr.RtrAddress,
                RtrCity = rtr.RtrCity,
                MinPrice = rtr.MinPrice,
                MaxPrice = rtr.MaxPrice,
                Stars = getStars(rtr.RtrID)

            });
            var selected = homeViewModels.Where(u => u.RtrRealName.ToLower().Contains(search.ToLower()));
            if (selected.Count() == 0)
            {
                ViewBag.Find = "Không tìm thấy kết quả";
            }
            return View(selected.ToList());
            
        }
        public ActionResult Notification()
        {
            return View();
        }
    }
}