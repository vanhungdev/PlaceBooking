using PlaceBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaceBooking.Controllers
{
    public class CommentController : Controller
    {
        PlaceBookingDbContext db = new PlaceBookingDbContext();
        // GET: MCQ
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin/Topic/Details/5                                                                                                                      
        public ActionResult Detail(int type, int topicId)
        {
            var listPost = db.Comments.Where(x=>x.Type == type && x.TopicId == topicId).OrderByDescending(x=>x.ID).ToList();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
            return View("Comment", listPost);
        }
        public void Create(Comment comment)
        {
            var userName = string.Empty;
            var userid = 0;
            if (Session["id"].Equals(""))
            {
                Message.set_flash("Bạn chưa đăng nhập.", "success");
                Response.Redirect("~/");
            }
            else
            {
                userName = Session["user"].ToString();
                userid = int.Parse(Session["id"].ToString());
            }

            // Validate
            comment.CreateDate = DateTime.Now;
            comment.Status = 1;
            comment.UserName = userName;
            comment.UserId = userid;
            db.Comments.Add(comment);
            db.SaveChanges();
            Message.set_flash("Thêm bình luận thành công", "success");
            if (comment.Type == 1)
            {
                Response.Redirect($"~/room-detail/{comment.TopicId}");
            }
            else
            {
                Response.Redirect($"~/room-detail/{comment.TopicId}");
            }
        }
    }
}                                    