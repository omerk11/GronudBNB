using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Models
{
    public class ApartmentOwner
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public int Age { get; set; }
        public ICollection<Apartment> Apartments { get; set; }//One-To-One?


    }
}
