using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GeekGang.Models;

namespace GeekGang.ViewModels
{
    public class UserMeetupViewModel
    {
        public List<User> users { get; set; }
        public List<Meetup> meetups { get; set; }
    }
}