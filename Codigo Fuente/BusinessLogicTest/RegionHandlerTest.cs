using BusinessLogic;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicTest
{
    [TestClass]
    public class RegionHandlerTest
    {
        private Region regionCentro;
        private Region regionCorredor;

        [TestInitialize]
        public void SetUp()
        {
            regionCentro = new Region { Name =  "Region Centro Sur" };
            regionCorredor = new Region { Name =  "Region Corredor Pajaros Pintados" };
        }

        [TestMethod]
        public void GetAllRegions()
        {
            var mock = new Mock<IRepository<Region>>(MockBehavior.Strict);
            var handler = new RegionHandler(mock.Object);

            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).Returns(new List<Region> { regionCentro, regionCorredor });

            List<Region> res = handler.GetAll();

            mock.VerifyAll();
            Assert.AreEqual(regionCentro, res[0]);
            Assert.AreEqual(regionCorredor, res[1]);
            Assert.AreEqual(2, res.Count);
        }
    }
}
