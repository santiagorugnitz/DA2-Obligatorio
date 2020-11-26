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
        private Accommodation accommodation;
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
                Image = new Image { Name = "imagen" },
                Region = new Region() { Name =  "Region Centro Sur" },
                TouristSpotCategories = new List<TouristSpotCategory> { new TouristSpotCategory() { Category = new Category { Name = "Ciudades" } } }

            };

            accommodation = new Accommodation()
            {
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                Images = new List<Image> { new Image { Name = "imagen" } },
                Fee = 4000,
                Description = "Hotel in Mvdeo",
                Telephone = "+598",
                ContactInformation = "Owner",
                TouristSpot = touristSpot
            };


            reservation = new Reservation()
            {
                Id = 1,
                Accommodation = accommodation,
                CheckIn = DateTime.Today.AddDays(1),
                CheckOut = DateTime.Today.AddDays(10),

                Adults = new Tuple<int, int>(2, 0),
                ChildrenQuantity = 1,
                BabyQuantity = 0,
                Name = "Martin",
                Surname = "Gutman",
                Email = "martin.gut",
                ReservationState = ReservationState.Accepted,
            };
        }


        [TestMethod]
        public void AddReservation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accommodation>().Add(accommodation);
                repo.Add(reservation);

                Assert.AreEqual("martin.gut", repo.GetAll().First().Email);

                context.Set<Reservation>().Remove(reservation);
                context.Set<Accommodation>().Remove(accommodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void DeleteReservation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accommodation>().Add(accommodation);
                context.Set<Reservation>().Add(reservation);
                context.SaveChanges();

                repo.Delete(reservation);
                context.Set<Accommodation>().Remove(accommodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();

                Assert.AreEqual(0, repo.GetAll().Count());
            }
        }

        [TestMethod]
        public void GetAllReservations()
        {

            Reservation reservation2 = new Reservation()
            {
                Id = 2,
                Accommodation = accommodation,
                CheckIn = DateTime.Today.AddDays(1),
                CheckOut = DateTime.Today.AddDays(10),

                Adults = new Tuple<int, int>(2, 0),
                ChildrenQuantity = 1,
                BabyQuantity = 0,
                Name = "Martin",
                Surname = "Gutman",
                Email = "martin.gut",
                ReservationState = ReservationState.Accepted,
            };

            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accommodation>().Add(accommodation);
                context.Set<Reservation>().Add(reservation);
                context.Set<Reservation>().Add(reservation2);

                context.SaveChanges();

                var res = repo.GetAll();

                Assert.AreEqual(true, res.Contains(reservation));
                Assert.AreEqual(true, res.Contains(reservation2));
                Assert.AreEqual(2, res.Count());

                context.Set<Reservation>().Remove(reservation);
                context.Set<Reservation>().Remove(reservation2);
                context.Set<Accommodation>().Remove(accommodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetReservation()
        {

            Reservation reservation2 = new Reservation()
            {
                Id = 2,
                Accommodation = accommodation,
                CheckIn = DateTime.Today.AddDays(1),
                CheckOut = DateTime.Today.AddDays(10),

                Adults = new Tuple<int, int>(2, 0),
                ChildrenQuantity = 1,
                BabyQuantity = 0,
                Name = "Martin",
                Surname = "Gutman",
                Email = "santi.rug",
                ReservationState = ReservationState.Accepted,
            };

            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accommodation>().Add(accommodation);
                context.Set<Reservation>().Add(reservation);
                context.Set<Reservation>().Add(reservation2);


                var res = repo.Get(1);

                Assert.AreEqual("martin.gut", res.Email);
                
                context.Set<Reservation>().Remove(reservation);
                context.Set<Reservation>().Remove(reservation2);
                context.Set<Accommodation>().Remove(accommodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void UpdateReservation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Reservation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accommodation>().Add(accommodation);
                context.Set<Reservation>().Add(reservation);
                context.SaveChanges();

                reservation.Email = "santi.rug";

                repo.Update(reservation);
                
                Assert.AreEqual("santi.rug", repo.GetAll().First().Email);

                context.Set<Reservation>().Remove(reservation);
                context.Set<Accommodation>().Remove(accommodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }
    }
}
