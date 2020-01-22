using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeekGang.Models;

namespace GeekGang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            GeekGangContext db = new GeekGangContext();
            return View(from Meetup in db.Meetups.Take(9) select Meetup);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}