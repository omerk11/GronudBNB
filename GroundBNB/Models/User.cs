using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required(ErrorMessage = "ID number required")]
        public int ID { get; set; }
        [Required(ErrorMessage = "First Name required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name required")]
        public string LastName { get; set; }
        [Range(18,120)]
        [Required(ErrorMessage = "Age required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Phone Number required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email address required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The password is required")]
        public string Password { get; set; }
        public ICollection<Apartment> Apartments { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public bool IsAdmin { get; set; }


    }
}
