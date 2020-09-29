using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccess
{
    public class TourismContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Accomodation> Accomodations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public TourismContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Administrator>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Accomodation>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Reservation>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Reservation>()
                .HasOne(x => x.Accomodation);
                
            modelBuilder.Entity<Accomodation>()
                .HasOne(x => x.TouristSpot);
        }



    }
}
