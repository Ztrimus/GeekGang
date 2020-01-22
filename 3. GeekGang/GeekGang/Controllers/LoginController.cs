using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeekGang.Models;

namespace GeekGang.Controllers
{
    public class LoginController : Controller
    {
        GeekGangContext db = new GeekGangContext();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        } 
        
        [HttpPost]
        public ActionResult Autherize(User userModel)
        {
            var checkMail = db.Users.Where(x => x.email == userModel.email).FirstOrDefault();
            var userDetails = db.Users.Where(x => x.email == userModel.email && x.password == userModel.password).FirstOrDefault();

            if (checkMail == null)
            {
                userModel.LoginErrorMessage = "Account Doesn't exist. Please Sign up!";
                return View("Index", userModel);
            }
            if(userDetails == null)
            {
                userModel.LoginErrorMessage = "Wrong Password.";
                return View("Index", userModel);
            }
            else 
            {
                //Creating Section ID for user
                Session["userID"] = userDetails.id;
                Session["Name"] = String.Concat(userDetails.firstname, " ", userDetails.lastname);
                Session["Email"] = userDetails.email.ToString();
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult LogOut()
        {
            int userId = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}