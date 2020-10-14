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
    public class AccomodationRepositoryTest
    {
        DbContextOptions<TourismContext> options;
        private Accomodation accomodation;
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

            accomodation = new Accomodation()
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
        public void AddAccomodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accomodation>(context);


                context.Set<TouristSpot>().Add(touristSpot);

                repo.Add(accomodation);

                Assert.AreEqual(accomodation.Name, repo.GetAll().First().Name);

                context.Set<Accomodation>().Remove(accomodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();

            }
        }

        [TestMethod]
        public void DeleteAccomodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accomodation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accomodation>().Add(accomodation);
                context.SaveChanges();

                repo.Delete(accomodation);
                Assert.AreEqual(0, repo.GetAll().Count());

                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetAccomodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accomodation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accomodation>().Add(accomodation);
                context.SaveChanges();

                var res = repo.Get(accomodation.Id);

                Assert.AreEqual(accomodation.Id, res.Id);

                context.Set<Accomodation>().Remove(accomodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void UpdateAccomodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accomodation>(context);

                context.Set<TouristSpot>().Add(touristSpot);
                context.Set<Accomodation>().Add(accomodation);
                context.SaveChanges();

                accomodation.Available = false;

                repo.Update(accomodation);

                Assert.AreEqual(false, context.Set<Accomodation>().Find(accomodation.Id).Available);

                context.Set<Accomodation>().Remove(accomodation);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.SaveChanges();
            }
        }


        [TestMethod]
        public void GetAllFromTouristSpot()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Accomodation>(context);

                var region1 = new Region() { Name =  "Region Litoral Norte" };

                var touristSpot1 = new TouristSpot
                {
                    Id = 2,
                    Name = "Beach1",
                    Description = "asd1",
                    Image = new Image { Name = "imagen" },
                    Region = region1
                };

                var accomodation1 = new Accomodation()
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
                context.Set<Accomodation>().Add(accomodation);
                context.Set<Accomodation>().Add(accomodation1);

                context.SaveChanges();


                var res = repo.GetAll(x => ((Accomodation)x).TouristSpot.Equals(touristSpot) && ((Accomodation)x).Available).ToList();

                Assert.AreEqual(1, res[0].Id);

                context.Set<Accomodation>().Remove(accomodation);
                context.Set<Accomodation>().Remove(accomodation1);
                context.Set<TouristSpot>().Remove(touristSpot);
                context.Set<TouristSpot>().Remove(touristSpot1);
                context.Set<Region>().Remove(region1);
                context.SaveChanges();

            }
        }

    }
}
