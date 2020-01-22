using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeekGang.Models;

namespace GeekGang.Controllers
{
    public class SignupController : Controller
    {
        // GET: Signup
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(User new_user)
        {
            using (GeekGangContext db = new GeekGangContext())
            {
                if (db.Users.Any(x => x.email == new_user.email))
                {
                    new_user.LoginErrorMessage = "Email address already exist.";
                    return View("Index", new_user);
                }
                else
                {

                    db.Users.Add(new_user);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("Index", "Login");
                }
            }
        }
    }
}