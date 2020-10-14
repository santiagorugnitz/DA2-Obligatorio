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
    public class CategoryRepositoryTest
    {
        DbContextOptions<TourismContext> options;
        private Category categoryCiudad;
        private Category categoryPlaya;

        [TestInitialize]
        public void StartUp()
        {
            options = new DbContextOptionsBuilder<TourismContext>()
                             .UseInMemoryDatabase(databaseName: "TestDB")
                             .Options;

            categoryCiudad = new Category { Name = "Ciudad" };
            categoryPlaya = new Category { Name = "Playa" };
        }

        [TestMethod]
        public void GetAllCategories()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Category>(context);

                context.Set<Category>().Add(categoryPlaya);
                context.Set<Category>().Add(categoryCiudad);
                context.SaveChanges();

                var result = repo.GetAll();
                Assert.AreEqual("Playa", result.First().Name);
                Assert.AreEqual("Ciudad", result.Last().Name);
                Assert.AreEqual(2, result.Count());
                
                context.Set<Category>().Remove(categoryCiudad);
                context.Set<Category>().Remove(categoryPlaya);
                context.SaveChanges();
            }
        }

        [TestMethod]
        public void GetCategoryById()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Category>(context);

                context.Set<Category>().Add(categoryPlaya);
                context.Set<Category>().Add(categoryCiudad);
                context.SaveChanges();

                var result = repo.Get(categoryCiudad.Id);
                Assert.AreEqual("Ciudad", result.Name);
                
                context.Set<Category>().Remove(categoryCiudad);
                context.Set<Category>().Remove(categoryPlaya);
                context.SaveChanges();
            }
        }
    }
}
