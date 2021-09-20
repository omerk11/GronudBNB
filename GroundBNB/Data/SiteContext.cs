using GroundBNB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroundBNB.Data
{
    public class SiteContext : DbContext
    {
        public SiteContext(DbContextOptions<SiteContext> options):base(options)
        {

        }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentOwner> ApartmentOwners { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Apartment>().ToTable("Apartment");
        //    modelBuilder.Entity<ApartmentOwner>().ToTable("ApartmentOwner");
        //    modelBuilder.Entity<Guest>().ToTable("Guest");
        //    modelBuilder.Entity<Reservation>().ToTable("Reservation");
        //}
    }
}
