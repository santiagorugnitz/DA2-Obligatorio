using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DataAccess;
using Domain;
using System.Linq;
using System.Collections.Generic;

namespace DataAccessTest
{
    public class TouristSpotRepositoryTest
    {
        DbContextOptions<TourismContext> options;
        private TouristSpot spot;

        [TestInitialize]
        public void SetUp()
        {
            options = new DbContextOptionsBuilder<TourismContext>()
                             .UseInMemoryDatabase(databaseName: "TestDB")
                             .Options;

            spot = new TouristSpot()
            {
                Name = "Beach",
                Description = "asd",
                ImageUrl = "url",
                Region = new Region() { Name = "metropolitana" },
                Categories = new List<Category> { new Category { Name = "Ciudades" } }
            };
        }

        [TestMethod]
        public void AddAccomodation()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<TouristSpot>(context);

                repo.Add(spot);
                repo.Save();

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
                repo.Save();

                Assert.AreEqual(0, repo.GetAll().Count());

                context.Set<TouristSpot>().Remove(spot);
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

        [TestMethod]
        public void GetAllSpot()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<TouristSpot>(context);

                Category category = new Category
                {
                    Name = "Campo"
                };

                context.Set<TouristSpot>().Add(spot);
                spot.Categories.Clear();
                spot.Categories.Add(category);
                spot.Id++;
                context.Set<TouristSpot>().Add(spot);
                context.SaveChanges();

                var res = repo.GetAll(x => ((TouristSpot)x).Categories.Contains(category) && ((TouristSpot)x).Categories.Contains(category)).ToList();

                Assert.AreEqual(spot.Id, res[0].Id);

                context.Set<TouristSpot>().Remove(spot);
                spot.Id--;
                context.Set<TouristSpot>().Remove(spot);
                context.SaveChanges();
            }
        }

    }
}
