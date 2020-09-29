using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataAccess
{
    public class TourismContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
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
            
            modelBuilder.Entity<Reservation>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Reservation>()
                .HasOne(x => x.Accomodation);
        }



    }
}
