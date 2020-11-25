using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccess;
using Domain;
using System.Linq;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace DataAccessTest
{
    [TestClass]
    public class AccommodationRepositoryTest
    {
        DbContextOptions<TourismContext> options;
        private Accommodation accommodation;
        private TouristSpot touristSpot;
        private Region region;
        private Image image;

        [TestInitialize]
        public void StartUp()
        {
            options = new DbContextOptionsBuilder<TourismContext>()
                             .UseInMemoryDatabase(databaseName: "TestDB")
                             .Options;

            region = new Region() { Name =  "Region Centro Sur" };

            image = new Image { Name = "imagen" };

            touristSpot = new TouristSpot
            {
                Id = 1,
                Name = "Beach",
                Description = "asd",
                Image = image,
                Region = region
            };

            accommodation = new Accommodation()
            {
                Id = 1,
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                Available = true,
                Images = new List<Image> { image },
                Fee = 4000,
                Description = "Hotel in Mvdeo",
                Telephone = "+598",
                ContactInformation = "Owner",
                TouristSpot = touristSpot
            };

        }

        [TestCleanup]
        public void CleanUp()
        {
            using (var context = new TourismContext(options)) 
            {
                context.Set<Region>().Remove(region);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void Addaccommodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accommodation>(context);


                context.Set<TouristSpot>().Add(touristSpot);

                repo.Add(accommodation);

                Assert.AreEqual(accommodation.Name, repo.GetAll().First().Name);

                context.Set<Accommodation>().Remove(accommodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();

            }
        }

        [TestMethod]
        public void Deleteaccommodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accommodation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accommodation>().Add(accommodation);
                context.SaveChanges();

                repo.Delete(accommodation);
                Assert.AreEqual(0, repo.GetAll().Count());

                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void Getaccommodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accommodation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accommodation>().Add(accommodation);
                context.SaveChanges();

                var res = repo.Get(accommodation.Id);

                Assert.AreEqual(accommodation.Id, res.Id);

                context.Set<Accommodation>().Remove(accommodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void Updateaccommodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accommodation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accommodation>().Add(accommodation);
                context.SaveChanges();

                accommodation.Available = false;

                repo.Update(accommodation);

                Assert.AreEqual(false, context.Set<Accommodation>().Find(accommodation.Id).Available);

                context.Set<Accommodation>().Remove(accommodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }


        [TestMethod]
        public void GetAllFromTouristSpot()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accommodation>(context);

                var region1 = new Region() { Name =  "Region Litoral Norte" };

                var touristSpot1 = new TouristSpot
                {
                    Id = 2,
                    Name = "Beach1",
                    Description = "asd1",
                    Image = new Image { Name = "imagen" },
                    Region = region1
                };

                var accommodation1 = new Accommodation()
                {
                    Id = 2,
                    Name = "Hotel",
                    Stars = 4.0,
                    Address = "Cuareim",
                    Available = true,
                    Images = new List<Image> { new Image { Name = "imagen" } },
                    Fee = 4000,
                    Description = "Hotel in Mvdeo",
                    Telephone = "+598",
                    ContactInformation = "Owner",
                    TouristSpot = touristSpot1
                };
                context.Set<TouristSpot>().Add(touristSpot1);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accommodation>().Add(accommodation);
                context.Set<Accommodation>().Add(accommodation1);

                context.SaveChanges();


                var res = repo.GetAll(x => ((Accommodation)x).TouristSpot.Equals(touristSpot) && ((Accommodation)x).Available).ToList();

                Assert.AreEqual(1, res[0].Id);

                context.Set<Accommodation>().Remove(accommodation);
                context.Set<Accommodation>().Remove(accommodation1);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.Set<TouristSpot>().Remove(touristSpot1);
                context.Set<Region>().Remove(region1);
                context.SaveChanges();

            }
        }

    }
}
