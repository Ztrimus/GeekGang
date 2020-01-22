using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GeekGang.Models
{   
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Meetups = new HashSet<Meetup>();
            this.RSVPs = new HashSet<RSVP>();
        }
    
        public int id { get; set; }


        [DataType(DataType.Text)]
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter firstname."),MaxLength(10)]
        public string firstname { get; set; }


        [DataType(DataType.Text)]
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter firstname."), MaxLength(10)]
        public string lastname { get; set; }
        
        
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="You forget to add Email Addres.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string email { get; set; }


        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required...")]
        [MaxLength(12, ErrorMessage = "Do not enter more than 12 characters")]
        public string password { get; set; }


        [DataType(DataType.Password)]
        [DisplayName("Confirm Password")]
        [Compare("password")]
        public string confirmed { get; set; }


        public string LoginErrorMessage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Meetup> Meetups { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RSVP> RSVPs { get; set; }
    }
}
