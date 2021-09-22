using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Models
{
    public class Apartment
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Title required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Apartment Description required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Number Of Rooms required")]
        public int NumOfRooms { get; set; }
        [Required(ErrorMessage = "Price Per Day required")]
        public float PricePerDay { get; set; }
        [Required(ErrorMessage = "Apartment City required")]
        public string City { get; set; }
        [Required(ErrorMessage = "Apartment Street required")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Apartment Floor required")]
        public int Floor { get; set; }
        [Required(ErrorMessage = "Apartment Number required")]
        public int ApartmentNumber { get; set; }
        [Required(ErrorMessage = "Apartment Number required")]
        [Range(1,30)]
        public int MaxNumOfGuests { get; set; }
        [Required(ErrorMessage = "Owner ID required")]
        public int ApartmentOwnerID { get; set; }   
        public User ApartmentOwner { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
