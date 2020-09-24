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
    public class AccomodationHandlerTest
    {
        private Accomodation accomodation;
        private TouristSpot touristSpot;

        [TestInitialize]
        public void SetUp()
        {
            touristSpot = new TouristSpot
            {
                Name = "Beach",
                Description = "asd",
                ImageUrl = "url",
                Region = new Region() { Name = "region" },
                Categories = new List<Category>()
            };

            accomodation = new Accomodation()
            {
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                ImageUrlList = new List<string>(),
                Categories = new List<Category>(),
                Fee = 4000,
                Description = "Hotel in Mvdeo",
                Telephone = "+598",
                ContactInformation = "Owner",
                TouristSpot = touristSpot
            };
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException),
    "The tourist spot does not exists")]
        public void AddAccomodationWithoutTouristSpot()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Exists(touristSpot)).Returns(false);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            var res = handler.Add(accomodation);
        }

        [TestMethod]
        public void AddAccomodationWithTouristSpot()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Exists(touristSpot)).Returns(true);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            var res = handler.Add(accomodation);

            accomodationMock.VerifyAll();
            touristSpotMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void DeleteAccomodation()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            accomodationMock.Setup(x => x.Delete(accomodation)).Returns(true);

            var res = handler.Delete(accomodation);

            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void ExistsAccomodation()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            accomodationMock.Setup(x => x.Exists(accomodation)).Returns(true);

            var res = handler.Exists(accomodation);

            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }
    }
}
