using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeekGang.Models
{

    public partial class Meetup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meetup()
        {
            this.RSVPs = new HashSet<RSVP>();
            attendees_count = 0;
        }

        [DisplayName("Meetup ID")]
        public int id { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Title")]
        [Required(ErrorMessage = "Please enter meetup title."), MaxLength(100)]
        public string title { get; set; }

        [DisplayName("Hosted By")]
        public int host_id { get; set; }

        [DataType(DataType.Text)]
        [DisplayName("Venue")]
        [Required(ErrorMessage = "Please enter meetup venue."), MaxLength(50)]
        public string location { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayName("Choose Date Time")]
        [Required(ErrorMessage = "Please Select date and time.")]
        public DateTime date_time { get; set; }


        [DisplayName("Attendees Limit")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter Valid integer number.")]
        [Required(ErrorMessage = "Please enter limit on number of attendees.")]
        public int attendees_limit { get; set; }


        [DisplayName("Going Attendees")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter Valid integer number.")]
        public int attendees_count { get; set; }


        [DataType(DataType.Text)]
        [DisplayName("Tags")]
        [Required(ErrorMessage = "Please enter Tags/Keywords.")]
        public string tags { get; set; }


        [DataType(DataType.MultilineText)]
        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter Detail information about your meetup.")]
        public string detail { get; set; }
        public string LoginErrorMessage { get; set; }
        public string host_name { get; set; }


        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSVP> RSVPs { get; set; }
    }
}
