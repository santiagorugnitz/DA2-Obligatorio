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
    public class RegionRepositoryTest
    {
        DbContextOptions<TourismContext> options;
        private Region regionCentro;
        private Region regionCorredor;

        [TestInitialize]
        public void StartUp()
        {
            options = new DbContextOptionsBuilder<TourismContext>()
                             .UseInMemoryDatabase(databaseName: "TestDB")
                             .Options;

            regionCentro = new Region { Name = RegionName.Región_Centro_Sur };
            regionCorredor = new Region { Name = RegionName.Región_Corredor_Pájaros_Pintados };
        }

        [TestMethod]
        public void GetAllRegions()
        {
            using (var context = new TourismContext(options))
            {
                var repo = new Repository<Region>(context);

                context.Set<Region>().Add(regionCentro);
                context.Set<Region>().Add(regionCorredor);
                context.SaveChanges();

                var result = repo.GetAll();
                Assert.AreEqual(RegionName.Región_Centro_Sur, result.First().Name);
                Assert.AreEqual(RegionName.Región_Corredor_Pájaros_Pintados, result.Last().Name);
                Assert.AreEqual(2, result.Count());

                context.Set<Region>().Remove(regionCentro);
                context.Set<Region>().Remove(regionCorredor);
                context.SaveChanges();
            }
        }

    }
}
