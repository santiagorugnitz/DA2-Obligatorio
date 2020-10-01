﻿using BusinessLogic;
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

            TouristSpotCategory joinedEntry = new TouristSpotCategory()
            {
                Category = new Category
                {
                    Name = "Ciudades"
                }
            };

            touristSpot = new TouristSpot
            {
                Name = "Beach",
                Description = "asd",
                ImageUrl = "url",
                Region = new Region() { Name = RegionName.Región_Centro_Sur },
                TouristSpotCategories = new List<TouristSpotCategory> { joinedEntry }
            };

            accomodation = new Accomodation()
            {
                Id = 1,
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                Available = true,
                ImageUrlList = new List<string>(),
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
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object,joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns((TouristSpot)null);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            var res = handler.Add(accomodation);
        }

        [TestMethod]
        public void AddAccomodationWithTouristSpot()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            var res = handler.Add(accomodation);

            accomodationMock.VerifyAll();
            touristSpotMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Accomodation needs a non empty name")]
        public void AddAccomodationWithoutName()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            accomodation.Name = "";
            var res = handler.Add(accomodation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Accomodation needs a non empty name")]
        public void AddAccomodationWithoutName2()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            accomodation.Name = "   ";
            var res = handler.Add(accomodation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Accomodation needs a non empty address")]
        public void AddAccomodationWithoutDirection()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            accomodation.Address = "";
            var res = handler.Add(accomodation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Accomodation needs a non empty address")]
        public void AddAccomodationWithoutDirection2()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            accomodation.Address = "   ";
            var res = handler.Add(accomodation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation stars needs to be between 1 and 5")]
        public void AddAccomodationWithNegativeStars()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            accomodation.Stars = -1;
            var res = handler.Add(accomodation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation stars needs to be between 1 and 5")]
        public void AddAccomodationWithMoreThan5Stars()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            accomodation.Stars = 5.1;
            var res = handler.Add(accomodation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation stars needs to be between 1 and 5")]
        public void AddAccomodationWith0Stars()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            accomodation.Stars = 0;
            var res = handler.Add(accomodation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation fee needs to be more than 0")]
        public void AddAccomodationWith0Fee()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            accomodation.Fee = 0;
            var res = handler.Add(accomodation);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation fee needs to be more than 0")]
        public void AddAccomodationWithNegativeFee()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(true);

            accomodation.Fee = -10;
            var res = handler.Add(accomodation);
        }

        [TestMethod]
        public void DeleteAccomodation()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
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
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);

            var res = handler.Exists(accomodation);

            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void SearchAvailableAccomodation()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accomodation> { accomodation });

            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);
         
            var res = handler.SearchByTouristSpot(touristSpot, checkIn, checkOut);

            accomodationMock.VerifyAll();
            Assert.AreEqual(new List<Accomodation> { accomodation }[0], res[0]);
        }

        [TestMethod]
        public void SearchNonAvailableAccomodation()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accomodation> { });

            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);

            var res = handler.SearchByTouristSpot(touristSpot, checkIn, checkOut);

            accomodationMock.VerifyAll();
            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void ChangeAccomodationAvaliability()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);

            accomodationMock.Setup(x => x.Update(accomodation)).Returns(true);

            var res = handler.ChangeAvailability(accomodation, false);

            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }
    }
}
