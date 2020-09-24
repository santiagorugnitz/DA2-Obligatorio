using BusinessLogic;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace BusinessLogicTest
{
    [TestClass]
    public class TouristSpotHandlerTest
    {
        private TouristSpot spot;
        
        [TestInitialize]
        public void SetUp()
        {
            spot = new TouristSpot()
            {
                Name = "Beach",
                Description = "asd",
                ImageUrl = "url",
                Region = new Region() { Name = "region" },
                Categories = new List<Category>()
            };
        }

        [TestMethod]
        public void AddSpot()
        {
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var handler = new TouristSpotHandler(mock.Object);

            mock.Setup(x => x.Add(spot)).Returns(true);

            var res = handler.Add(spot);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void DeleteSpot()
        {
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var handler = new TouristSpotHandler(mock.Object);

            mock.Setup(x => x.Delete(spot)).Returns(true);

            var res = handler.Delete(spot);

            mock.VerifyAll();
            Assert.AreEqual(true, res);

        }

        [TestMethod]
        public void ExistsSpot()
        {
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var handler = new TouristSpotHandler(mock.Object);

            mock.Setup(x => x.Exists(spot)).Returns(true);

            var res = handler.Exists(spot);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }
    }
}
