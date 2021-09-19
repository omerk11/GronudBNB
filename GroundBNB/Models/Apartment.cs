using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Models
{
    public class Apartment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PricePerDay { get; set; }
        public string Address { get; set; }
        public ApartmentOwner Owner { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
