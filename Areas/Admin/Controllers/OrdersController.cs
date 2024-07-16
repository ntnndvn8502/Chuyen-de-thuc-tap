using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Simple.Models;

namespace Simple.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrdersController : Controller
    {
        private ApplicationDb db = new ApplicationDb();

        // GET: Admin/Orders
        public ActionResult Index(int? id)
        {
            ViewBag.id = id;
            if (id == null)
            {
                var orders = db.Orders.Include(o => o.Customer).Include(o => o.Restaurant);
                return View(orders.ToList());
            } else if (id == 1)
            {
                var orders = db.Orders.Include(o => o.Customer).Include(o => o.Restaurant)
                    .Where(u => u.Happen == true);
                return View(orders.ToList());
            } else if (id == 2)
            {
                var orders = db.Orders.Include(o => o.Customer).Include(o => o.Restaurant).
                    Where(u => u.UserAction == "Khách hàng không đến");
                return View(orders.ToList());
            } else if (id == 3)
            {
                var orders = db.Orders.Include(o => o.Customer).Include(o => o.Restaurant).
                    Where(u => u.UserAction == "Hủy bởi khách hàng");
                return View(orders.ToList());
            }
            else if (id == 4)
            {
                var orders = db.Orders.Include(o => o.Customer).Include(o => o.Restaurant).
                    Where(u => u.RtrAction == "Từ chối nhận đơn");
                return View(orders.ToList());
            }
            else if (id == 5)
            {
                var orders = db.Orders.Include(o => o.Customer).Include(o => o.Restaurant).
                    Where(u => u.RtrAction == "Đồng ý nhận đơn" && u.UserAction == "Đã đặt đơn" && u.Happen == false);
                return View(orders.ToList());
            } else
            {
                var orders = db.Orders.Include(o => o.Customer).Include(o => o.Restaurant).
                    Where(u => u.RtrAction == null && u.UserAction == "Đã đặt đơn");
                return View(orders.ToList());
            }


        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "CustomerUserName");
            ViewBag.RtrID = new SelectList(db.Restaurants, "RtrID", "RtrRealName");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,RtrID,CustomerId,ArrivalDate,ArrivalTime,BookTime,Quantity,OrderNote,UserAction,RtrAction")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "CustomerUserName", order.CustomerId);
            ViewBag.RtrID = new SelectList(db.Restaurants, "RtrID", "RtrRealName", order.RtrID);
            return View(order);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "CustomerUserName", order.CustomerId);
            ViewBag.RtrID = new SelectList(db.Restaurants, "RtrID", "RtrRealName", order.RtrID);
            return View(order);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,RtrID,CustomerId,ArrivalDate,ArrivalTime,BookTime,Quantity,OrderNote,UserAction,RtrAction")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerID", "CustomerUserName", order.CustomerId);
            ViewBag.RtrID = new SelectList(db.Restaurants, "RtrID", "RtrRealName", order.RtrID);
            return View(order);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
