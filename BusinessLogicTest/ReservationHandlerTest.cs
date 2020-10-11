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
        private Mock<IRepository<Accomodation>> accomodationMock;
        private Mock<IRepository<Region>> regionMock;
        private Mock<IRepository<Category>> categoryMock;
        private Mock<IRepository<Image>> imageMock;
        private Mock<IRepository<TouristSpot>> touristSpotMock;
        private Mock<IRepository<TouristSpotCategory>> joinedMock;
        private TouristSpotHandler touristSpotHandler;
        private AccomodationHandler accomodationHandler;

        [TestInitialize]
        public void SetUp()
        {
            touristSpot = new TouristSpot
            {
                Name = "Beach",
                Description = "asd",
                Image = new Image { Name = "imagen" },
                Region = new Region() { Name =  "Region Centro Sur" },
            };

            accomodation = new Accomodation()
            {
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                Images = new List<Image> { new Image { Name = "imagen" } },
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
                CheckIn = DateTime.Today.AddDays(1),
                CheckOut = DateTime.Today.AddDays(10),
                AdultQuantity = 2,
                ChildrenQuantity = 1,
                BabyQuantity = 0,
                Total= 90000,
                Name = "Martin",
                Surname = "Gutman",
                Email = "martin.gut",
                ReservationState = ReservationState.Aceptada,
                ReservationDescription = "Activa"
            };

            accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            regionMock = new Mock<IRepository<Region>>(MockBehavior.Loose);
            categoryMock = new Mock<IRepository<Category>>(MockBehavior.Loose);
            imageMock = new Mock<IRepository<Image>>(MockBehavior.Loose);
            touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, imageMock.Object,
                categoryMock.Object, regionMock.Object, joinedMock.Object);
            accomodationHandler = new AccomodationHandler(accomodationMock.Object, imageMock.Object, touristSpotHandler);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The accomodation spot does not exist")]
        public void AddReservationWithoutAccomodation()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns((Accomodation)null);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        public void AddReservationWithAccomodation()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            var res = handler.Add(reservation, accomodation.Id);

            mock.VerifyAll();
            accomodationMock.VerifyAll();
            Assert.AreEqual(reservation, res);
            Assert.AreEqual(reservation.Total, res.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Reservation needs a non empty name")]
        public void AddReservationWithoutName()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Name = "";
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Reservation needs a non empty name")]
        public void AddReservationWithoutName2()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Name = "    ";
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Reservation needs a non empty surname")]
        public void AddReservationWithoutSurname()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Surname = "";
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Reservation needs a non empty surname")]
        public void AddReservationWithoutSurname2()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Surname = "    ";
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Reservation needs a non empty email")]
        public void AddReservationWithoutEmail()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Email = "";
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Reservation needs a non empty email")]
        public void AddReservationWithoutEmail2()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Email = "    ";
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Reservation needs at least one adult guest")]
        public void AddReservationWithoutGuests()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.AdultQuantity = 0;
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Reservation needs at least one adult guest")]
        public void AddReservationWithNegativeGuests()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.AdultQuantity = -9;
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The children quantity must be 0 or more")]
        public void AddReservationWithNegativeChildren()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.ChildrenQuantity = -9;
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The baby quantity must be 0 or more")]
        public void AddReservationWithNegativeBabies()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.BabyQuantity = -9;
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Check In Date needs to be after today")]
        public void AddReservationWithIncorrectCheckInDate()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.CheckIn = DateTime.Today.AddDays(-1);
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Check Out Date needs to be after Check Out")]
        public void AddReservationWithIncorrectCheckOutDate()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);


            reservation.CheckOut = DateTime.Today.AddDays(1);
            reservation.CheckOut = DateTime.Today;
            var res = handler.Add(reservation, accomodation.Id);
        }

        [TestMethod]
        public void DeleteReservation()
        {
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
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            mock.Setup(x => x.Update(reservation)).Returns(true);
            mock.Setup(x => x.Get(reservation.Id)).Returns(reservation);

            var res = handler.ChangeState(reservation.Id, ReservationState.Creada, "Cambio de estado");

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void ChangeReservationStateWrongId()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accomodationHandler);

            mock.Setup(x => x.Get(reservation.Id)).Returns((Reservation)null);

            var res = handler.ChangeState(reservation.Id, ReservationState.Creada, "Cambio de estado");

        }
    }
}
