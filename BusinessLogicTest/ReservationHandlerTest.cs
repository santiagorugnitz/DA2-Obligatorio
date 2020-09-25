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
    public class ReservationHandlerTest
    {

        private Accomodation accomodation;
        private TouristSpot touristSpot;
        private Reservation reservation;
        private GuestsQuantity guestsQuantity;

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

            GuestsQuantity guestsQuantity = new GuestsQuantity
            {
                AdultQuantity = 2,
                ChildrenQuantity = 1,
                BabyQuantity = 0
            };

            reservation = new Reservation()
            {
                Id = 1,
                Accomodation = accomodation,
                CheckIn = new DateTime(),
                CheckOut = new DateTime().AddDays(10),
                GuestsQuantity = guestsQuantity,
                Name = "Martin",
                Surname = "Gutman",
                Email = "martin.gut",
                ReservationState = ReservationState.Aceptada,
                ReservationDescription = "Activa"
            };
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException),
    "The accomodation spot does not exists")]
        public void AddReservationWithoutAccomodation()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object);
            var accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            touristSpotMock.Setup(x => x.Exists(touristSpot)).Returns(true);
            accomodationMock.Setup(x => x.Exists(accomodation)).Returns(false);
            mock.Setup(x => x.Add(reservation)).Returns(true);

            var res = handler.Add(reservation);
        }

        [TestMethod]
        public void AddReservationWithAccomodation()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Strict);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object);
            var accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Exists(accomodation)).Returns(true);
            mock.Setup(x => x.Add(reservation)).Returns(true);

            var res = handler.Add(reservation);

            mock.VerifyAll();
            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void DeleteReservation()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Strict);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object);
            var accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            mock.Setup(x => x.Delete(reservation)).Returns(true);

            var res = handler.Delete(reservation);

            mock.VerifyAll();
            Assert.AreEqual(true, res);

        }

        [TestMethod]
        public void CheckReservationState()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Strict);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object);
            var accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            mock.Setup(x => x.GetById(reservation.Id)).Returns(reservation);

            Reservation res = handler.CheckState(reservation.Id);

            mock.VerifyAll();
            Assert.AreEqual(ReservationState.Aceptada, res.ReservationState);
            Assert.AreEqual("Activa", res.ReservationDescription);
        }

        [TestMethod]
        public void ChangeReservationState()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Strict);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object);
            var accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            mock.Setup(x => x.Modify(reservation.Id, reservation)).Returns(true);

            var res = handler.ChangeState(reservation, ReservationState.Creada, "Cambio de estado");

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }
    }
}
