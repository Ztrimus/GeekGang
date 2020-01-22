using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeekGang.Models;
using GeekGang.ViewModels;
using PagedList;
using PagedList.Mvc;

namespace GeekGang.Controllers
{
    public class MeetupController : Controller
    {
        GeekGangContext db = new GeekGangContext();
        // GET: Meetup
        public ActionResult Index(int? page)
        {
            int current_user_id = Convert.ToInt32(Session["userID"]);
            var all_meetups = db.Meetups;
            foreach(var meetup in all_meetups)
            {
                var host_details = db.Users.Find(meetup.host_id);
                if(host_details.id == current_user_id)
                {
                    meetup.host_name = "By You";
                }
                else
                {
                    meetup.host_name = host_details.firstname + " " + host_details.lastname;
                }
                
            }
            return View(all_meetups.ToList().ToPagedList(page ?? 1, 3));
        }

        public ActionResult Details(int id)
        {
            // Get Host Name
            var host_id = db.Users.Find(db.Meetups.Find(id).host_id);
            var user_id = Convert.ToInt32(Session["userID"]);
            var user_name = host_id.firstname + " " + host_id.lastname;
            ViewBag.host_name = user_name;
            ViewBag.attendees_count = db.RSVPs.Count(x => x.meet_id == id && x.status == "Accepted");

            // Check whether current user already registered or not
            if (Session["userID"] != null)
            {
                var rsvp_status = db.RSVPs.Where(x => x.meet_id == id && x.user_id == user_id).FirstOrDefault();
                ViewBag.rsvp_status = rsvp_status == null ? "Attend" : rsvp_status.status;
            }

            //var meetup = db.Meetups.Where(i => i.id == id);
            //var single_meetup = meetup.Skip(id).First();
            //return View(single_meetup);

            Meetup meetup = db.Meetups.Find(id);
            return View(meetup);

            //var meet = db.Meetups.Find(id);
            //return Content(Convert.ToString(meet.GetType()));

            //return Content(Convert.ToString(id));
        }

        public ActionResult MyMeetups()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                int user_id = Convert.ToInt32(Session["userID"]);
                var my_meetups = db.Meetups.Where(m => m.host_id == user_id);
                return View(my_meetups);
                //return Content(Convert.ToString(Session["userID"].GetType()));
            }
            
        }

        public ActionResult Attend(int id)
        {
            RSVP new_registration = new RSVP
            {
                meet_id = id,
                user_id = Convert.ToInt32(Session["userID"]),
                status = "Pending"
            };

            db.RSVPs.Add(new_registration);
            db.SaveChanges();
            ModelState.Clear();
            TempData["Message"] = "Request sent.";

            return RedirectToAction("Details","Meetup", new { id=id});
        }

        public ActionResult Cancel(int id)
        {
            int user_id = Convert.ToInt32(Session["userID"]);
            var rsvp_status = db.RSVPs.Where(x => x.meet_id == id && x.user_id == user_id).FirstOrDefault();
            db.RSVPs.Remove(rsvp_status);
            db.SaveChanges();
            ModelState.Clear();
            TempData["Message"] = "Registration cancelled.";

            return RedirectToAction("Details", "Meetup", new { id = id });
        }
    }
}