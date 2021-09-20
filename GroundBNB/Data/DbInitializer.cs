using GroundBNB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SiteContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.ApartmentOwners.Any())
            {
                return;   // DB has been seeded
            }
            var apartments = new Apartment[]
            {

            };
            foreach(Apartment ap in apartments)
            {
                context.Apartments.Add(ap);
            }
            context.SaveChanges();

            var apartmentOwners = new ApartmentOwner[]
            {

            };
            foreach (ApartmentOwner owner in apartmentOwners)
            {
                context.ApartmentOwners.Add(owner);
            }
            context.SaveChanges();

            var guests = new Guest[]
            {

            };
            foreach (Guest guest in guests)
            {
                context.Guests.Add(guest);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {

            };
            foreach (Reservation res in reservations)
            {
                context.Reservations.Add(res);
            }
            context.SaveChanges();
        }
    }
}
