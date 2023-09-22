using PlaceBooking.Common;
using PlaceBooking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaceBooking.Controllers
{
    public class ModulesController : Controller
    {
        // GET: Modules
        PlaceBookingDbContext db = new PlaceBookingDbContext();
        public ActionResult _Header1()
        {
            if (!String.IsNullOrEmpty((string)Session["userName11"]))
            {
                ViewBag.sessionFullname = Session["userName11"];
            }    
            return View("_Header1");
        }
        public ActionResult _Mainmenu()
        {
            var list = db.Menus.Where(m => m.Status == 1 && m.Parentid == 0).ToList();
            return View("_Mainmenu", list);
        }
        public ActionResult _Footer()
        {
            return View("_Footer");
        }
    }
}