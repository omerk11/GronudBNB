using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Range(1, 5)]
        public int? Rating { get; set; }
        //review member
        public int NumberOdGuests { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime PurchseDate { get; set; }
        public Apartment Apartment { get; set; }
        public Guest Guest { get; set; }
    }
}
