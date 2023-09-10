using PlaceBooking.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using API_NganLuong;
using PlaceBooking.nganluonAPI;

namespace PlaceBooking.Controllers
{
    public class CheckoutController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        BanVeXeDbContext db = new BanVeXeDbContext();
        // GET: Checkout
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            var list = new List<Ticket>();
            var id1 = string.IsNullOrEmpty(Session["id"].ToString()) ? 0 : int.Parse(Session["id"].ToString());
            if (id1 == 0)
            {
                Message.set_flash("Vui lòng đang nhập ", "danger");
                return Redirect("~/dang-nhap");
            }
            int id = int.Parse(fc["datve"]);
            var list1 = db.Tickets.Find(id);
            list.Add(list1);
            ViewBag.ve1 = id;
            return View("", list.ToList());
        }
        [HttpPost]
        public ActionResult checkOut(Order order, FormCollection fc)
        {
            string orderCode ="MaVE"+DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second;
            Session["OrderId"] = orderCode;
            float total = float.Parse(fc["total"]);
            int id1 = int.Parse(fc["veOnvay"]);
            string veReturn = fc["veReturn"];
            string payment_method = Request["option_payment"];
            if (payment_method.Equals("COD"))
            {
                // cap nhat thong tin sau khi dat hang thanh cong
                SaveOrder(order, total, id1, veReturn, "Thanh toán tiền mặt", 3, orderCode);
                return View("checkOutComfin", order);
            }

            return View("checkOutComfin", order);
        }
        // lay thong tin cac ve da book
        public ActionResult _BookingConnfig(int orderId)
        {
            var list = db.Ordersdetails.Where(m => m.Orderid == orderId).ToList();
            var list1 = new List<Ticket>();
            foreach (var item in list)
            {
                Ticket ticket = db.Tickets.Find(item.TicketId);
                list1.Add(ticket);
            }

            return View("_BookingConnfig", list1.ToList());
        }

        public void SaveOrder(Order order, float total,int id1,string veReturn, string paymentMethod, int StatusPayment, string ordercode)
        {
            // tru so luong nghe
            var ticket = db.Tickets.Find(id1);
            order.CreateDate = DateTime.Now;
            order.Status = 2;
            order.UserId = string.IsNullOrEmpty(Session["id"].ToString()) ? 0 : int.Parse(Session["id"].ToString()) ;
            order.Total = total;
            order.DeliveryPaymentMethod = paymentMethod;
            order.StatusPayment = StatusPayment;
            order.Code = ordercode;
            order.NameRoom = ticket.Name;
            order.RoomId = id1;
            db.Orders.Add(order);
            db.SaveChanges();
            int lastOrderID = order.Id;
            Ordersdetail orderDetail = new Ordersdetail();
            orderDetail.TicketId = id1;
            orderDetail.Orderid = lastOrderID;
            db.Ordersdetails.Add(orderDetail);

            //ticket.guestTotal = ticket.guestTotal - order.guestTotal;
            ticket.IsBooking = 1;
            db.Entry(ticket).State = EntityState.Modified;
            //neu ton tai ve 2 chieu
            if (!string.IsNullOrEmpty(veReturn))
            {
                int id2 = int.Parse(veReturn);
                Ordersdetail orderDetail2 = new Ordersdetail();
                orderDetail2.TicketId = id2;
                orderDetail2.Orderid = lastOrderID;
                db.Ordersdetails.Add(orderDetail2);
                // tru so luong nghe
                var ticket2 = db.Tickets.Find(id2);
                ticket2.GuestTotal = ticket2.GuestTotal - order.GuestTotal;
                db.Entry(ticket2).State = EntityState.Modified;
            }
            db.SaveChanges();
            if(order.StatusPayment == 3)
            {
                // neeu chon hinh thuc thanh toan Mua ves
                //SendEmail(order.email, order.name);
            }
          
        }

        //Khi huy thanh toán Ngan Luong
        public ActionResult cancel_order()
        {

            return View("cancel_order");
        }
        //Khi thanh toán Ngan Luong XOng
        public ActionResult confirm_orderPaymentOnline()
        {
            String Token = Request["token"];
            RequestCheckOrder info = new RequestCheckOrder();
            info.Merchant_id = nganluongInfo.Merchant_id;
            info.Merchant_password = nganluongInfo.Merchant_password;
            info.Token = Token;
            APICheckoutV3 objNLChecout = new APICheckoutV3();
            ResponseCheckOrder result = objNLChecout.GetTransactionDetail(info);
            if (result.errorCode == "00")
            {
                String codeOrder = Session["OrderId"].ToString();
                var OrderInfo = db.Orders.OrderByDescending(m => m.Code == codeOrder).FirstOrDefault();
                OrderInfo.StatusPayment = 1;
                db.Entry(OrderInfo).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.paymentStatus = OrderInfo.StatusPayment;
                ViewBag.Methodpayment = OrderInfo.DeliveryPaymentMethod;
                //send email
                //SendEmail(OrderInfo.email, OrderInfo.name);
                return View("checkOutComfin", OrderInfo);
            }
            else
            {
                ViewBag.status = false;
            }

            return View("confirm_orderPaymentOnline");
        }

        //Khi huy thanh toán MOMO
        public ActionResult cancel_order_momo()
        {

            return View("cancel_order");
        }
        //Khi Thanh toám momo xong
        public ActionResult confirm_orderPaymentOnline_momo()
        {

            String errorCode = Request["errorCode"];
            String orderId = Request["orderId"];
            if (errorCode == "0")
            {
                Session["SessionCart"] = null;
                var OrderInfo = db.Orders.Where(m => m.Code == orderId).FirstOrDefault();
                OrderInfo.StatusPayment = 1;
                db.Entry(OrderInfo).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.paymentStatus = OrderInfo.StatusPayment;
                ViewBag.Methodpayment = OrderInfo.DeliveryPaymentMethod;
                //send mail
                //SendEmail(OrderInfo.email, OrderInfo.name);
                return View("checkOutComfin", OrderInfo);
            }
            else
            {
                ViewBag.status = false;
            }
            return View("confirm_orderPaymentOnline");
        }
/*        public void SendEmail(string CustomerEmail, string CustomerName)
        {
            MailMessage mm = new MailMessage(Util.email, CustomerEmail);
            mm.Subject = "[YATRA.COM] THÔNG BÁO XÁC NHẬN ĐƠN Đặt phòng";
            mm.Body = "Xin chào " + CustomerName + "," +
                " YATRA.COM vừa nhận được đơn đặt hàng từ bạn, chúng tôi sẽ gọi bạn để xác minh thông tin đặt hàng sớm nhất có thể." +
                " Cảm ơn bạn đã Đặt phòng tại website, Chúc bạn một ngày tốt lành!";
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            /// Email dùng để gửi đi
            NetworkCredential nc = new NetworkCredential(Util.email, Util.password);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nc;
            smtp.Send(mm);
        }*/
    }
}