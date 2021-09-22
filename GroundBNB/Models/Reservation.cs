using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Models
{
    public class Reservation
    {
        public int ID { get; set; }
        [Range(1, 5)]
        public int? Rating { get; set; }
        public string? Review { get; set; }
        public int NumberOfGuests { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PurchseDate { get; set; }
        public int ApartmentID { get; set; }
        public int GuestID { get; set; }    
        public Apartment Apartment { get; set; }
        public User Guest { get; set; }//List of guests or just reservation owner?
    }
}
