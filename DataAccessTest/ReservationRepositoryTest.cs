using DataAccess;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessTest
{
    [TestClass]
    public class ReservationRepositoryTest
    {
        private Accomodation accomodation;
        private TouristSpot touristSpot;
        private Reservation reservation;

        DbContextOptions<TourismContext> options;

        [TestInitialize]
        public void StartUp()
        {
            options = new DbContextOptionsBuilder<TourismContext>()
                             .UseInMemoryDatabase(databaseName: "TestDB")
                             .Options;

            touristSpot = new TouristSpot
            {
                Name = "Beach",
                Description = "asd",
                ImageUrl = "url",
                Region = new Region() { Name = "region" },
                TouristSpotCategories = new List<TouristSpotCategory> { new TouristSpotCategory() { Category = new Category { Name = "Ciudades" } } }
            };

            accomodation = new Accomodation()
            {
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                ImageUrlList = new List<string>(),
                Fee = 4000,
                Description = "Hotel in Mvdeo",
                Telephone = "+598",
                ContactInformation = "Owner",
                TouristSpot = touristSpot
            };


            reservation = new Reservation()
            {
                Id = 1,
                Accomodation = accomodation,
                CheckIn = new DateTime(),
                CheckOut = new DateTime().AddDays(10),
                AdultQuantity = 2,
                ChildrenQuantity = 1,
                BabyQuantity = 0,
                Name = "Martin",
                Surname = "Gutman",
                Email = "martin.gut",
                ReservationState = ReservationState.Aceptada,
                ReservationDescription = "Activa"
            };
        }


        [TestMethod]
        public void AddReservation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accomodation>().Add(accomodation);
                repo.Add(reservation);
                repo.Save();

                Assert.AreEqual("martin.gut", repo.GetAll().First().Email);

                context.Set<Reservation>().Remove(reservation);
                context.Set<Accomodation>().Remove(accomodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
                repo.Save();
            }
        }

        [TestMethod]
        public void DeleteReservation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accomodation>().Add(accomodation);
                context.Set<Reservation>().Add(reservation);
                repo.Save();

                Assert.AreEqual("martin.gut", repo.GetAll().First().Email);

                repo.Delete(reservation);
                context.Set<Accomodation>().Remove(accomodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
                repo.Save();

                Assert.AreEqual(0, repo.GetAll().Count());
            }
        }

        [TestMethod]
        public void GetAllReservations()
        {

            Reservation reservation2 = new Reservation()
            {
                Id = 2,
                Accomodation = accomodation,
                CheckIn = new DateTime(),
                CheckOut = new DateTime().AddDays(10),
                AdultQuantity = 2,
                ChildrenQuantity = 1,
                BabyQuantity = 0,
                Name = "Martin",
                Surname = "Gutman",
                Email = "martin.gut",
                ReservationState = ReservationState.Aceptada,
                ReservationDescription = "Activa"
            };

            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accomodation>().Add(accomodation);
                context.Set<Reservation>().Add(reservation);
                context.Set<Reservation>().Add(reservation2);

                repo.Save();

                var res = repo.GetAll();

                Assert.AreEqual(true, res.Contains(reservation));
                Assert.AreEqual(true, res.Contains(reservation2));
                Assert.AreEqual(2, res.Count());

                context.Set<Reservation>().Remove(reservation);
                context.Set<Reservation>().Remove(reservation2);
                context.Set<Accomodation>().Remove(accomodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
                repo.Save();
            }
        }

        [TestMethod]
        public void GetReservation()
        {

            Reservation reservation2 = new Reservation()
            {
                Id = 2,
                Accomodation = accomodation,
                CheckIn = new DateTime(),
                CheckOut = new DateTime().AddDays(10),
                AdultQuantity = 2,
                ChildrenQuantity = 1,
                BabyQuantity = 0,
                Name = "Martin",
                Surname = "Gutman",
                Email = "santi.rug",
                ReservationState = ReservationState.Aceptada,
                ReservationDescription = "Activa"
            };

            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accomodation>().Add(accomodation);
                context.Set<Reservation>().Add(reservation);
                context.Set<Reservation>().Add(reservation2);

                repo.Save();

                var res = repo.Get(1);

                Assert.AreEqual("martin.gut", res.Email);
                
                context.Set<Reservation>().Remove(reservation);
                context.Set<Reservation>().Remove(reservation2);
                context.Set<Accomodation>().Remove(accomodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
                repo.Save();
            }
        }

        [TestMethod]
        public void UpdateReservation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accomodation>().Add(accomodation);
                context.Set<Reservation>().Add(reservation);
                repo.Save();

                reservation.Email = "santi.rug";

                repo.Update(reservation);
                
                Assert.AreEqual("santi.rug", repo.GetAll().First().Email);

                context.Set<Reservation>().Remove(reservation);
                context.Set<Accomodation>().Remove(accomodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
                repo.Save();
            }
        }
    }
}
