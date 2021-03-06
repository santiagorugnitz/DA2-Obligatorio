using BusinessLogic;
using DataAccessInterface;
using Domain;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace BusinessLogicTest
{
    [TestClass]
    public class ReservationHandlerTest
    {

        private Accommodation accommodation;
        private TouristSpot touristSpot;
        private Reservation reservation;
        private Mock<IRepository<Accommodation>> accommodationMock;
        private Mock<IRepository<Region>> regionMock;
        private Mock<IRepository<Category>> categoryMock;
        private Mock<IRepository<Image>> imageMock;
        private Mock<IRepository<TouristSpot>> touristSpotMock;
        private Mock<IRepository<TouristSpotCategory>> joinedMock;
        private TouristSpotHandler touristSpotHandler;
        private AccommodationHandler accommodationHandler;

        [TestInitialize]
        public void SetUp()
        {
            touristSpot = new TouristSpot
            {
                Name = "Beach",
                Description = "asd",
                Image = new Image { Name = "imagen" },
                Region = new Region() { Name = "Region Centro Sur" },
            };

            accommodation = new Accommodation()
            {
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                Images = new List<Image> { new Image { Name = "imagen" } },
                Fee = 100,
                Description = "Hotel in Mvdeo",
                Telephone = "+598",
                ContactInformation = "Owner",
                TouristSpot = touristSpot,
                Available = true
            };


            reservation = new Reservation()
            {
                Id = 1,
                Accommodation = accommodation,
                CheckIn = DateTime.Today.AddDays(1),
                CheckOut = DateTime.Today.AddDays(10),
                Adults = new Tuple<int, int>(2, 3),
                ChildrenQuantity = 1,
                BabyQuantity = 4,
                Total = 200 + 50 + 100 + 200 + 70,
                Name = "Martin",
                Surname = "Gutman",
                Email = "martin.gut",
                ReservationState = ReservationState.Accepted,
            };

            accommodationMock = new Mock<IRepository<Accommodation>>(MockBehavior.Loose);
            regionMock = new Mock<IRepository<Region>>(MockBehavior.Loose);
            categoryMock = new Mock<IRepository<Category>>(MockBehavior.Loose);
            imageMock = new Mock<IRepository<Image>>(MockBehavior.Loose);
            touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object,
                categoryMock.Object, regionMock.Object, joinedMock.Object);
            accommodationHandler = new AccommodationHandler(accommodationMock.Object, touristSpotHandler);

        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation spot does not exist")]
        public void AddReservationWithoutAccommodation()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns((Accommodation)null);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
"The accommodation spot does not exist")]
        public void AddReservationWithoutAvailableAccommodation()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodation.Available = false;

            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns((Accommodation)null);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        public void AddReservationWithAccommodation()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);


            var res = handler.Add(reservation, accommodation.Id);

            mock.VerifyAll();
            accommodationMock.VerifyAll();
            Assert.AreEqual(reservation, res);
            Assert.AreEqual(reservation.Total, res.Total);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Reservation needs a non empty name")]
        public void AddReservationWithoutName()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Name = "";
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Reservation needs a non empty name")]
        public void AddReservationWithoutName2()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Name = "    ";
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Reservation needs a non empty surname")]
        public void AddReservationWithoutSurname()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Surname = "";
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Reservation needs a non empty surname")]
        public void AddReservationWithoutSurname2()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Surname = "    ";
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Reservation needs a non empty email")]
        public void AddReservationWithoutEmail()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Email = "";
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Reservation needs a non empty email")]
        public void AddReservationWithoutEmail2()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Email = "    ";
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Reservation needs at least one adult guest")]
        public void AddReservationWithoutGuests()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Adults = new Tuple<int, int>(0, 0);
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Reservation needs at least one adult guest")]
        public void AddReservationWithNegativeGuests()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Adults = new Tuple<int, int>(-1, 1);

            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The children quantity must be 0 or more")]
        public void AddReservationWithNegativeChildren()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.ChildrenQuantity = -9;
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The baby quantity must be 0 or more")]
        public void AddReservationWithNegativeBabies()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.BabyQuantity = -9;
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void AddReservationWithNegativeRetiredGuests()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.Adults = new Tuple<int, int>(1, -1);
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Check In Date needs to be after today")]
        public void AddReservationWithIncorrectCheckInDate()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.CheckIn = DateTime.Today.AddDays(-1);
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Check Out Date needs to be after Check Out")]
        public void AddReservationWithIncorrectCheckOutDate()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);


            reservation.CheckOut = DateTime.Today.AddDays(1);
            reservation.CheckOut = DateTime.Today;
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "Invalid state")]
        public void AddReservationWithIncorrectState()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            mock.Setup(x => x.Add(reservation)).Returns(reservation);

            reservation.ReservationState = (ReservationState)10;
            var res = handler.Add(reservation, accommodation.Id);
        }

        [TestMethod]
        public void DeleteReservation()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            mock.Setup(x => x.Delete(reservation)).Returns(true);

            var res = handler.Delete(reservation);

            mock.VerifyAll();
            Assert.AreEqual(true, res);

        }

        [TestMethod]
        public void CheckReservationState()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            mock.Setup(x => x.Get(reservation.Id)).Returns(reservation);

            Reservation res = handler.CheckState(reservation.Id);

            mock.VerifyAll();
            Assert.AreEqual(ReservationState.Accepted, res.ReservationState);
        }

        [TestMethod]
        public void ChangeReservationState()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            mock.Setup(x => x.Update(reservation)).Returns(true);
            mock.Setup(x => x.Get(reservation.Id)).Returns(reservation);

            var res = handler.ChangeState(reservation.Id, ReservationState.Created, "Cambio de estado");

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void ChangeReservationStateWrongId()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            mock.Setup(x => x.Get(reservation.Id)).Returns((Reservation)null);

            var res = handler.ChangeState(reservation.Id, ReservationState.Created, "Cambio de estado");
        }

        [ExpectedException(typeof(BadRequestException))]
        [TestMethod]
        public void ChangeReservationStateWrongState()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);
            mock.Setup(x => x.Get(reservation.Id)).Returns(reservation);

            var res = handler.ChangeState(reservation.Id, (ReservationState)5, "Cambio de estado");

        }

        [TestMethod]
        public void Review()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            mock.Setup(x => x.Update(reservation)).Returns(true);
            mock.Setup(x => x.Get(reservation.Id)).Returns(reservation);

            var res = handler.Review(reservation.Id, 2, "ok");

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void ReviewWrongId()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            mock.Setup(x => x.Get(reservation.Id)).Returns((Reservation)null);

            var res = handler.Review(reservation.Id, 2, "ok");
        }

        [ExpectedException(typeof(BadRequestException))]
        [TestMethod]
        public void ReviewWrongScore()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);
            mock.Setup(x => x.Get(reservation.Id)).Returns(reservation);

            var res = handler.Review(reservation.Id, 0, "ok");

        }
        [ExpectedException(typeof(BadRequestException))]
        [TestMethod]
        public void ReviewWrongScore2()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);
            mock.Setup(x => x.Get(reservation.Id)).Returns(reservation);

            var res = handler.Review(reservation.Id, 5.1, "ok");

        }

        [TestMethod]
        public void GetAllFromAccommodationOk()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).Returns(new List<Reservation>() { reservation });
            accommodationMock.Setup(x => x.Get(reservation.Accommodation.Id)).Returns((Accommodation)reservation.Accommodation);

            var res = handler.GetAllFromAccommodation(reservation.Accommodation.Id);

            mock.VerifyAll();
            accommodationMock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void GetAllFromNonExistingAccommodation()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object, accommodationHandler);

            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).Returns(new List<Reservation>() { reservation });
            accommodationMock.Setup(x => x.Get(reservation.Accommodation.Id)).Returns((Accommodation)null);

            var res = handler.GetAllFromAccommodation(reservation.Accommodation.Id);
        }

    }
}
