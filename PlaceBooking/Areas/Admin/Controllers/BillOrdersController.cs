using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PlaceBooking.Common;
using PlaceBooking.Models;

namespace PlaceBooking.Areas.Admin.Controllers
{
    public class BillOrdersController : BaseController
    {
        private PlaceBookingDbContext db = new PlaceBookingDbContext();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            return View(db.BillOrders.Where(m=>m.Status!=0).ToList());
        }

        public ActionResult Create()
        {
            ViewBag.listtopic = db.Orders.Where(m => m.Status != 0).ToList();

            BillOrder bill = new BillOrder();
            return View(bill);
        }

        // POST: Admin/Topic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BillOrder mtopic)
        {
            if (ModelState.IsValid)
            {

                Order order = db.Orders.Where(x => x.Code == mtopic.OrderCode).FirstOrDefault();
                //category
                mtopic.CreateDate = DateTime.Now;
                mtopic.Total = (decimal)order.Total + mtopic.Surcharge + mtopic.ElectricityBillTotal + mtopic.WaterMoneyTotal;
                mtopic.UserId = order.UserId;
                mtopic.ServiceID = 1;
                db.BillOrders.Add(mtopic);
                db.SaveChanges();
                Message.set_flash("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            Message.set_flash("Thêm thất bại", "danger");
            ViewBag.listtopic = db.Orders.Where(m => m.Status != 0).ToList();
            return View(mtopic);
        }
        public ActionResult Status(int id)
        {
            BillOrder mtopic = db.BillOrders.Find(id);
            mtopic.Status = (mtopic.PaymentStatus == 1) ? 2 : 1;
            db.Entry(mtopic).State = EntityState.Modified;
            db.SaveChanges();
            Message.set_flash("Thay đổi trang thái thành công", "success");
            return RedirectToAction("Index");
        }
    }
}
