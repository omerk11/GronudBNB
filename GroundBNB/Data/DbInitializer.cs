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
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            context.Apartments.RemoveRange(context.Apartments.ToList());
            context.Users.RemoveRange(context.Users.ToList());
            context.Reservations.RemoveRange(context.Reservations.ToList());
            var apartments = new Apartment[]
            {

            };
            foreach(Apartment ap in apartments)
            {
                context.Apartments.Add(ap);
            }
            context.SaveChanges();

            var users = new User[]
            {

            };
            foreach (User user in users)
            {
                context.Users.Add(user);
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
