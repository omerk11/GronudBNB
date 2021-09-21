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

            //context.Apartments.RemoveRange(context.Apartments.ToList());
            //context.Users.RemoveRange(context.Users.ToList());
            //context.Reservations.RemoveRange(context.Reservations.ToList());
            var users = new User[]
            {
                new User{ID=12345678, FirstName="Shay", LastName="Horovitz", Age=18, PhoneNumber = "0501234567", Email="shay@bhay.com", Password="password", IsAdmin = false},
                new User{ID=87654321, FirstName="Ido", LastName="Ros", Age=18, PhoneNumber = "0501234566", Email="ido@bhay.com", Password="password", IsAdmin = true}
            };
            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();


            var apartments = new Apartment[]
            {
                new Apartment{ Title="Beit Halomotai", Description="Mamlechet ha yeladim", NumOfRooms = 1,City="Givat Brener", Street="anaharef", Floor = 0, ApartmentNumber = 1, ApartmentOwnerID = 12345678 ,PricePerDay = 32}
            };
            foreach(Apartment ap in apartments)
            {
                context.Apartments.Add(ap);
            }

               context.SaveChanges();
        

            var reservations = new Reservation[]
            {
                new Reservation{NumberOfGuests = 1, StartDate = DateTime.Parse("15/10/2007"),EndDate = DateTime.Parse("15/10/2008"), PurchseDate = DateTime.Parse("15/10/2006") ,ApartmentID = 1, GuestID = 87654321}
            };
            foreach (Reservation res in reservations)
            {
                context.Reservations.Add(res);
            }
            context.SaveChanges();
        }
    }
}
