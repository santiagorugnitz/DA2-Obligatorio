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
  

        [TestInitialize]
        public void SetUp()
        {
            touristSpot = new TouristSpot
            {
                Name = "Beach",
                Description = "asd",
                ImageUrl = "url",
                Region = new Region() { Name = RegionName.Región_Centro_Sur },
            };

            accomodation = new Accomodation()
            {
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                ImageUrlList = new List<string>(),
                Fee = 4000,
                Description = "Hotel in Mvdeo",
                Telephone = "+598",
                ContactInformation = "Owner",
                TouristSpot = touristSpot
            };


            reservation = new Reservation()
            {
                Id = 1,
                Accomodation = accomodation,
                CheckIn = new DateTime(),
                CheckOut = new DateTime().AddDays(10),
                AdultQuantity = 2,
                ChildrenQuantity = 1,
                BabyQuantity = 0,
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
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns((Accomodation)null);
            mock.Setup(x => x.Add(reservation)).Returns(true);

            var res = handler.Add(reservation);
        }

        [TestMethod]
        public void AddReservationWithAccomodation()
        {
            var accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Strict);
            var touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
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
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
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
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            mock.Setup(x => x.Get(reservation.Id)).Returns(reservation);

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
            var joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            var touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, joinedMock.Object);
            var accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            mock.Setup(x => x.Update(reservation)).Returns(true);

            var res = handler.ChangeState(reservation, ReservationState.Creada, "Cambio de estado");

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }
    }
}
