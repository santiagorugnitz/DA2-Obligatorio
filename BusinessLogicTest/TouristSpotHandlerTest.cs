﻿using BusinessLogic;
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
        private TouristSpotCategory joinedEntry;

        [TestInitialize]
        public void SetUp()
        {

            joinedEntry = new TouristSpotCategory()
            {
                Category = new Category
                {
                    Name = "Ciudades"
                }
            };

            spot = new TouristSpot()
            {
                Name = "Beach",
                Description = "asd",
                ImageUrl = "url",
                Region = new Region() { Name = "metropolitana" },
                TouristSpotCategories = new List<TouristSpotCategory> { joinedEntry }
   
            };
        }

        [TestMethod]
        public void AddSpot()
        {
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var handler = new TouristSpotHandler(mock.Object, joinedMock.Object);
            mock.Setup(x => x.Add(spot)).Returns(true);

            var res = handler.Add(spot);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void DeleteSpot()
        {
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var handler = new TouristSpotHandler(mock.Object, joinedMock.Object);

            mock.Setup(x => x.Delete(spot)).Returns(true);

            var res = handler.Delete(spot);

            mock.VerifyAll();
            Assert.AreEqual(true, res);

        }

        [TestMethod]
        public void ExistsSpot()
        {
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);

            var handler = new TouristSpotHandler(mock.Object, joinedMock.Object);

            mock.Setup(x => x.Get(spot.Id)).Returns(spot);

            var res = handler.Exists(spot);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void SearchByRegion()
        {
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var handler = new TouristSpotHandler(mock.Object, joinedMock.Object);

            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).Returns(new List<TouristSpot> { spot });

            List<TouristSpot> res = handler.SearchByRegion(new Region { Name = "metropolitana" });

            mock.VerifyAll();
            Assert.AreEqual(spot, res[0]);
        }

        [TestMethod]
        public void SearchByCategoriy()
        {
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var handler = new TouristSpotHandler(mock.Object, joinedMock.Object);

            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).Returns(new List<TouristSpot> { spot });
            joinedMock.Setup(x => x.Get(0)).Returns(joinedEntry);

            List<TouristSpot> res = handler.SearchByCategory(new Category { Name = "Ciudades" });

            mock.VerifyAll();
            joinedMock.VerifyAll();
            Assert.AreEqual(spot, res[0]);
        }

        [TestMethod]
        public void SearchByRegionAndCategoriy()
        {
            var mock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var handler = new TouristSpotHandler(mock.Object, joinedMock.Object);

            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).Returns(new List<TouristSpot> { spot });
            joinedMock.Setup(x => x.Get(0)).Returns(joinedEntry);

            List<TouristSpot> res = handler.SearchByRegionAndCategory(new Category { Name = "ciudades" },
                new Region { Name = "metropolitana" });


            joinedMock.VerifyAll();
            mock.VerifyAll();
            Assert.AreEqual(spot, res[0]);
        }
    }
}
