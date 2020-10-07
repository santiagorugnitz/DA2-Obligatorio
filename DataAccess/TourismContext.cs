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
        public DbSet<TouristSpot> TouristSpots { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }

        public TourismContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
        optionBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Region>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Category>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TouristSpot>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<TouristSpot>()
                .HasOne(x => x.Image);

            modelBuilder.Entity<TouristSpotCategory>()
                .HasKey(x => new { x.TouristSpotId, x.CategoryId});

            modelBuilder.Entity<TouristSpotCategory>()
                .HasOne(x => x.TouristSpot)
                .WithMany(x => x.TouristSpotCategories)
                .HasForeignKey(x => x.TouristSpotId);

            modelBuilder.Entity<TouristSpotCategory>()
                .HasOne(x => x.Category)
                .WithMany(x => x.TouristSpotCategories)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<Administrator>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Accomodation>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Accomodation>()
                .HasOne(x => x.TouristSpot);

            modelBuilder.Entity<Reservation>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Reservation>()
                .HasOne(x => x.Accomodation);

            modelBuilder.Entity<Accomodation>()
                .HasOne(x => x.TouristSpot);

            modelBuilder.Entity<Accomodation>()
                .HasMany(x => x.Images);
        }



    }
}
