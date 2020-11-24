using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccess;
using Domain;
using System.Linq;
using System.Collections.Generic;

namespace DataAccessTest
{
    [TestClass]
    public class TouristSpotRepositoryTest
    {
        DbContextOptions<TourismContext> options;
        private TouristSpot spot;
        private Image image;
        private Category category;
        private Region region;

        [TestInitialize]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<TourismContext>()
                             .UseInMemoryDatabase(databaseName: "TestDB")
                             .Options;

            image = new Image()
            {
                Id=1,
                Name = "image"
            };
             category = new Category
            {
                Id = 1,
                Name = "Campo"
            };
            region = new Region() { Name = "Region metropolitana" };
            spot = new TouristSpot()
            {
                Id=1,
                Name = "Beach",
                Description = "asd",
                Image = image,
                Region = region,
                TouristSpotCategories = new List<TouristSpotCategory> { new TouristSpotCategory() { Category = category } }
            };
        }

        [TestCleanup]
        public void CleanUp()
        {
            using(var context = new TourismContext(options))
            {
                context.Set<Image>().Remove(image);
                context.Set<Category>().Remove(category);
                context.Set<Region>().Remove(region);
                context.SaveChanges();

            }
        }

        [TestMethod]
        public void GetAllSpot()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<TouristSpot>(context);
                var joinedRepo = new Repository<TouristSpotCategory>(context);
                var catRepo = new Repository<Category>(context);



                context.Set<TouristSpot>().Add(spot);

                TouristSpot spot1 = new TouristSpot()
                {
                    Id=2,
                    Name = "Beach1",
                    Description = "asd1",
                    Image = image,
                    Region = region,
                    TouristSpotCategories = new List<TouristSpotCategory> { new TouristSpotCategory() { Category = new Category { Name = "Ciudades" } } }
                };

                context.Set<TouristSpot>().Add(spot1);
                context.SaveChanges();

                var joinedEntry = context.Set<TouristSpotCategory>().Find(category.Id, spot.Id);

                var res = repo.GetAll(x => ((TouristSpot)x).Id == joinedEntry.TouristSpotId && ((TouristSpot)x).Region.Id == spot.Region.Id).ToList();

                Assert.AreEqual(1, res.Count());
                Assert.AreEqual(spot.Id, res[0].Id);

                context.Set<TouristSpot>().Remove(spot);
                context.Set<TouristSpot>().Remove(spot1);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void Addaccommodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<TouristSpot>(context);

                repo.Add(spot);

                Assert.AreEqual(spot.Name, repo.GetAll().First().Name);

                context.Set<TouristSpot>().Remove(spot);
                context.SaveChanges();

            }
        }

        [TestMethod]
        public void DeleteSpot()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<TouristSpot>(context);

                context.Set<TouristSpot>().Add(spot);
                context.SaveChanges();

                repo.Delete(spot);

                Assert.AreEqual(0, repo.GetAll().Count());

                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetSpot()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<TouristSpot>(context);

                context.Set<TouristSpot>().Add(spot);
                context.SaveChanges();

                var res = repo.Get(spot.Id);

                Assert.AreEqual(spot.Id, res.Id);

                context.Set<TouristSpot>().Remove(spot);
                context.SaveChanges();
            }
        }

        


    }
}
