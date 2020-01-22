using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GeekGang.Models;

namespace GeekGang.Controllers
{
    public class HostController : Controller
    {
        GeekGangContext db = new GeekGangContext();


        public ActionResult Index()
        {
            if (Session["userID"] == null)
                return RedirectToAction("Index", "Login");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(Meetup new_meetup)
        {
            var update_meetup = db.Meetups.Where(x => x.id == new_meetup.id).FirstOrDefault();
            if(update_meetup == null)
            {
                new_meetup.host_id = Convert.ToInt32(Session["userID"]);
                new_meetup.host_name = Convert.ToString(Session["Name"]);
                db.Meetups.Add(new_meetup);
                db.SaveChanges();
                ModelState.Clear();
                TempData["Message"] = "Successfully hosted your meet.";
            }
            else
            {
                update_meetup.title = new_meetup.title;
                update_meetup.location = new_meetup.location;
                update_meetup.date_time = new_meetup.date_time;
                update_meetup.attendees_limit = new_meetup.attendees_limit;
                update_meetup.tags = new_meetup.tags;
                update_meetup.detail = new_meetup.detail;
                
                db.SaveChanges();
            }

            return RedirectToAction("Details", "Meetup", new { id=new_meetup.id});
        }

        public ActionResult Delete(int id)
        {
            int current_user_id = Convert.ToInt32(Session["userID"]);
            var meetup_details = db.Meetups.Where(x=>x.id==id && x.host_id==current_user_id).FirstOrDefault();
            // First delete all RSVPs related to this meetup
            foreach(var rsvp in db.RSVPs.Where(x => x.meet_id == id))
            {
                db.RSVPs.Remove(rsvp);
            }           
            db.Meetups.Remove(meetup_details);
            db.SaveChanges();
            ModelState.Clear();

            return RedirectToAction("MyMeetups", "Meetup");
        }

        public ActionResult Update(int? id)
        {
            if(Session["useriD"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var meetup = db.Meetups.Where(x => x.id == id).FirstOrDefault();
                return View(meetup);

            }
        }

        public ActionResult UserRequests(int? id)
        {
            if(id == null || Session["userID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var manage_requests = db.RSVPs.Where(x=> x.meet_id == id);
            foreach(var request in manage_requests)
            {
                var user_details = db.Users.Find(request.user_id);
                request.user_name = user_details.firstname + " " + user_details.lastname;
            }
            TempData["Meetup_id"] = id;
            return View(manage_requests);
        }

        public ActionResult ChangeStatus(int id, string status)
        {
            var user_request = db.RSVPs.First(x => x.id == id);
            user_request.status = status;
            db.SaveChanges();
            return RedirectToAction("UserRequests", "Host", new { id = user_request.meet_id});
        }
    }
}