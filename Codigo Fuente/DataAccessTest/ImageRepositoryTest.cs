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
    public class ImageRepositoryTest
    {
        private DbContextOptions options;

        [TestInitialize]
        public void StartUp()
        {
            options = new DbContextOptionsBuilder<TourismContext>()
                             .UseInMemoryDatabase(databaseName: "TestDB")
                             .Options;
        }

        [TestMethod]
        public void GetImageById()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Image>(context);

                Image image = new Image { Name = "image" };

                context.Set<Image>().Add(image);
                context.SaveChanges();

                var result = repo.Get(image.Id);
                Assert.AreEqual("image", result.Name);

                context.Set<Image>().Remove(image);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetImageByName()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Image>(context);

                Image image = new Image { Id = 1, Name = "image" };

                context.Set<Image>().Add(image);
                context.SaveChanges();

                var result = repo.GetAll(x => ((Image) x).Name == image.Name);
                
                Assert.AreEqual(1, result.ToList().First().Id);

                context.Set<Image>().Remove(image);
                context.SaveChanges();
            }
        }
    }
}
