using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Phone { get; set; }
        public int Age { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
