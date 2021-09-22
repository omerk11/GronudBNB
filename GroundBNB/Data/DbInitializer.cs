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

            var users = new User[]
            {
                new User{ID=12345678, FirstName="Patrick", LastName="Star", Age=18, PhoneNumber = "0501234567", Email="patric@gmail.com", Password="password", IsAdmin = false},
                new User{ID=87654321, FirstName="Ido", LastName="Rosenberger", Age=20, PhoneNumber = "0501234566", Email="idor@gmail.com", Password="password", IsAdmin = true},
                new User{ID=19735819, FirstName="Omer", LastName="Keren", Age=20, PhoneNumber = "0504658134", Email="omerk@gmail.com", Password="password", IsAdmin = true},
                new User{ID=73219567, FirstName="Tal", LastName="Folkman", Age=20, PhoneNumber = "0502655887", Email="talf@gmail.com", Password="password", IsAdmin = true},
                new User{ID=79865315, FirstName="Jesus", LastName="Christ", Age=45, PhoneNumber = "054342654", Email="jesusk@heaven.com", Password="password", IsAdmin = false},
                new User{ID=37659187, FirstName="Benjamin", LastName="Netanyahu", Age=73, PhoneNumber = "0524578424", Email="bibi@gov.com", Password="password", IsAdmin = false},
                new User{ID=31316495, FirstName="Gordon", LastName="Ramsay", Age=48, PhoneNumber = "0533771244", Email="gordonr@gmail.com", Password="password", IsAdmin = false},
                new User{ID=79461352, FirstName="Yonit", LastName="Levi", Age=37, PhoneNumber = "050663546/", Email="yonitl@gmail.com", Password="password", IsAdmin = false},
                new User{ID=94835746, FirstName="James", LastName="Bond", Age=48, PhoneNumber = "0504438226", Email="jamesb@gmail.com", Password="password", IsAdmin = false},
                new User{ID=81291855, FirstName="Ori", LastName="Geller", Age=35, PhoneNumber = "0502535487", Email="orig@gmail.com", Password="password", IsAdmin = false},
                new User{ID=81908957, FirstName="Slender", LastName="Man", Age=100, PhoneNumber = "0502645356", Email="slendy@gmail.com", Password="password", IsAdmin = false},
                new User{ID=41859632, FirstName="Thommy", LastName="Shalby", Age=48, PhoneNumber = "0506666666", Email="tommys@gmail.com", Password="password", IsAdmin = false},
                new User{ID=85761565, FirstName="Hikaru", LastName="Nakamura", Age=38, PhoneNumber = "0546352441", Email="hikarun@gmail.com", Password="password", IsAdmin = false},
                new User{ID=39984456, FirstName="Beny", LastName="Goren", Age=73, PhoneNumber = "0501374567", Email="benyg@gmail.com", Password="password", IsAdmin = false},
                new User{ID=91286535, FirstName="Steve", LastName="Jobs", Age=82, PhoneNumber = "0509182375", Email="stivej@apple.com", Password="password", IsAdmin = false},
                new User{ID=39881445, FirstName="Mark", LastName="Zuckerberg", Age=28, PhoneNumber = "0542637516", Email="markz@facebook.com", Password="password", IsAdmin = false},
                new User{ID=92818564, FirstName="Donfil", LastName="Shmonfil", Age=62, PhoneNumber = "0502626265", Email="donfil@gmail.com", Password="password", IsAdmin = false}
            };
            foreach (User user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();


            var apartments = new Apartment[]
            {
                new Apartment{ Title="Beit Halomotai", Description="Kingdom of kids", NumOfRooms = 1,City="Givat Brener", Street="HaShita St 2", Floor = 0, ApartmentOwnerID = 12345678 ,PricePerDay = 783, MaxNumOfGuests =2},
                new Apartment{ Title="Collage Of Management", Description="Fun vacation house with the amazing views of the sand dunes", NumOfRooms = 4,City="Rishon LeZion", Street="Elie Wiesel St 2", Floor = 0, ApartmentNumber = 5, ApartmentOwnerID = 12345678 ,PricePerDay = 82, MaxNumOfGuests = 20},
                new Apartment{ Title="Relaxing Tropical Getaway In Ramle", Description="Free parking & Pet friendly", NumOfRooms = 2,City="Ramle", Street="Zahal St 5", Floor = 2, ApartmentNumber = 7, ApartmentOwnerID = 12345678 ,PricePerDay = 485, MaxNumOfGuests = 15},
                new Apartment{ Title="Studio Loft Downtown TLV", Description="", NumOfRooms = 3,City="Tel Aviv", Street="Hulda 7", Floor = 3, ApartmentNumber = 11, ApartmentOwnerID = 12345678 ,PricePerDay = 435, MaxNumOfGuests = 8},
                new Apartment{ Title="Luxury Penthouse Apartment", Description="", NumOfRooms = 6,City="Tel Aviv", Street="Shai Agnon St 8", Floor = 17, ApartmentNumber = 67, ApartmentOwnerID = 41859632 ,PricePerDay =124, MaxNumOfGuests =9},
                new Apartment{ Title="Close to everything!!", Description="", NumOfRooms = 3,City="Giv'at Ela", Street="HaCarmel St 1", Floor = 2, ApartmentNumber = 7, ApartmentOwnerID = 41859632 ,PricePerDay = 168, MaxNumOfGuests =4},
                new Apartment{ Title="Studio Jerusalem", Description="", NumOfRooms = 2,City="Jerusalem", Street="Negba St 8", Floor = 8, ApartmentNumber = 32, ApartmentOwnerID = 41859632 ,PricePerDay = 738, MaxNumOfGuests =3},
                new Apartment{ Title="The getaway", Description="", NumOfRooms = 6,City="Mitzpe Ramon", Street="Ein Shaviv St 37", Floor = 5, ApartmentNumber = 23, ApartmentOwnerID = 41859632 ,PricePerDay = 435, MaxNumOfGuests =6},
                new Apartment{ Title="Beachfront apartment", Description="", NumOfRooms = 3,City="Beer Sheva", Street="Etsel St 2", Floor = 3, ApartmentNumber = 11, ApartmentOwnerID = 37659187 ,PricePerDay = 364, MaxNumOfGuests =8},
                new Apartment{ Title="Luxurious modern studio apartment", Description="", NumOfRooms = 2,City="Haifa", Street="Nurit St 46", Floor = 2, ApartmentNumber = 5, ApartmentOwnerID = 37659187 ,PricePerDay = 274, MaxNumOfGuests =7},
                new Apartment{ Title="Fabulous City View Unit", Description="", NumOfRooms = 5,City="Eilat", Street="Kida St 6", Floor = 0, ApartmentNumber = 1, ApartmentOwnerID = 19735819 ,PricePerDay = 324, MaxNumOfGuests =9},
                new Apartment{ Title="2 Bedroom Suite", Description="", NumOfRooms = 8,City="Tel Aviv", Street="HaAvoda St 20", Floor = 18, ApartmentNumber = 72, ApartmentOwnerID = 19735819 ,PricePerDay = 745, MaxNumOfGuests =18},
                new Apartment{ Title="Amazing Apartment", Description="", NumOfRooms = 5,City="Rishon LeZion", Street="HaNagid St 9", Floor = 7, ApartmentNumber = 26, ApartmentOwnerID = 19735819 ,PricePerDay = 242, MaxNumOfGuests =28}
            };
            foreach(Apartment ap in apartments)
            {
                context.Apartments.Add(ap);
            }

               context.SaveChanges();
        

            var reservations = new Reservation[]
            {
                new Reservation{NumberOfGuests = 1, StartDate = DateTime.Parse("15/10/2020"),EndDate = DateTime.Parse("18/10/2020"), PurchseDate = DateTime.Parse("15/08/2020") ,ApartmentID = 1, GuestID = 92818564},
                new Reservation{NumberOfGuests = 2, StartDate = DateTime.Parse("15/01/2021"),EndDate = DateTime.Parse("19/01/2021"), PurchseDate = DateTime.Parse("15/09/2020") ,ApartmentID = 1, GuestID = 92818564},
                new Reservation{NumberOfGuests = 5, StartDate = DateTime.Parse("15/10/2020"),EndDate = DateTime.Parse("18/10/2020"), PurchseDate = DateTime.Parse("15/09/2020") ,ApartmentID = 3, GuestID = 39984456},
                new Reservation{NumberOfGuests = 10, StartDate = DateTime.Parse("03/01/2021"),EndDate = DateTime.Parse("19/01/2021"), PurchseDate = DateTime.Parse("15/11/2020") ,ApartmentID = 3, GuestID = 81291855},
                new Reservation{NumberOfGuests = 8, StartDate = DateTime.Parse("15/11/2021"),EndDate = DateTime.Parse("15/12/2021"), PurchseDate = DateTime.Parse("07/10/2021") ,ApartmentID = 5, GuestID = 81291855},//
                new Reservation{NumberOfGuests = 2, StartDate = DateTime.Parse("22/03/2021"),EndDate = DateTime.Parse("26/03/2021"), PurchseDate = DateTime.Parse("21/03/2021") ,ApartmentID = 7, GuestID =39984456 },
                new Reservation{NumberOfGuests = 4, StartDate = DateTime.Parse("11/10/2021"),EndDate = DateTime.Parse("11/11/2021"), PurchseDate = DateTime.Parse("15/09/2021") ,ApartmentID = 8, GuestID = 85761565},
                new Reservation{NumberOfGuests = 6, StartDate = DateTime.Parse("25/07/2021"),EndDate = DateTime.Parse("11/08/2021"), PurchseDate = DateTime.Parse("25/05/2021") ,ApartmentID = 10, GuestID =85761565 },
                new Reservation{NumberOfGuests = 6, StartDate = DateTime.Parse("15/03/2022"),EndDate = DateTime.Parse("25/03/2022"), PurchseDate = DateTime.Parse("14/03/2022") ,ApartmentID = 11, GuestID = 92818564}
            };
            foreach (Reservation res in reservations)
            {
                context.Reservations.Add(res);
            }
            context.SaveChanges();
        }
    }
}
