using BusinessLogic;
using DataAccessInterface;
using Domain;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicTest
{
    [TestClass]
    public class ReportHandlerTest
    {
        [TestClass]
        public class ReservationHandlerTest
        {

            private accommodation accommodation;
            private TouristSpot touristSpot;
            private Reservation reservation;
            private Mock<IRepository<accommodation>> accommodationMock;
            private Mock<IRepository<Region>> regionMock;
            private Mock<IRepository<Category>> categoryMock;
            private Mock<IRepository<Image>> imageMock;
            private Mock<IRepository<TouristSpot>> touristSpotMock;
            private Mock<IRepository<TouristSpotCategory>> joinedMock;
            private Mock<IRepository<Reservation>> reservationMock;
            private TouristSpotHandler touristSpotHandler;
            private accommodationHandler accommodationHandler;
            private ReservationHandler reservationHandler;
            private ReportHandler reportHandler;

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

                accommodation = new accommodation()
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
                    accommodation = accommodation,
                    CheckIn = DateTime.Today.AddDays(1),
                    CheckOut = DateTime.Today.AddDays(10),
                    Adults = new Tuple<int, int>(2, 1),
                    BabyQuantity = 0,
                    Total = 90000,
                    Name = "Martin",
                    Surname = "Gutman",
                    Email = "martin.gut",
                    ReservationState = ReservationState.Accepted,
                };

                accommodationMock = new Mock<IRepository<accommodation>>(MockBehavior.Strict);
                regionMock = new Mock<IRepository<Region>>(MockBehavior.Loose);
                categoryMock = new Mock<IRepository<Category>>(MockBehavior.Loose);
                imageMock = new Mock<IRepository<Image>>(MockBehavior.Loose);
                touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
                joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
                reservationMock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
                touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object,
                    categoryMock.Object, regionMock.Object, joinedMock.Object);
                accommodationHandler = new accommodationHandler(accommodationMock.Object, touristSpotHandler);
                reservationHandler = new ReservationHandler(reservationMock.Object, accommodationHandler);
                reportHandler = new ReportHandler(accommodationHandler, reservationHandler);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException),
    "The starting date must be before the finishing one")]
            public void ReportWithIncorrectDates()
            {
                reportHandler.accommodationsReport(1, DateTime.Now, DateTime.Now.AddDays(-1));
            }

            [TestMethod]
            public void ReportWithoutaccommodations()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation>());

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-1));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithReservationWithNonIncludedDate1()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now;
                reservation.CheckOut = DateTime.Now.AddDays(1);

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithReservationWithNonIncludedDate2()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now.AddDays(4);
                reservation.CheckOut = DateTime.Now.AddDays(5);

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithReservationWithIncludedDate1()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now;
                reservation.CheckOut = DateTime.Now.AddDays(2);

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().accommodation, accommodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithReservationWithIncludedDate2()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now.AddDays(2);
                reservation.CheckOut = DateTime.Now.AddDays(4);

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().accommodation, accommodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithReservationWithIncludedDate3()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now.AddDays(2);
                reservation.CheckOut = DateTime.Now.AddDays(4);

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().accommodation, accommodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithReservationWithIncludedDate4()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now;
                reservation.CheckOut = DateTime.Now.AddDays(1);

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().accommodation, accommodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithReservationWithNonIncludedState1()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.ReservationState = ReservationState.Rejected;

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-1));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithReservationWithNonIncludedState2()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.ReservationState = ReservationState.Expired;

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-1));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithOneReservationForOneaccommodation()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().accommodation, accommodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithTwoReservationForOneaccommodation()
            {
                Reservation reservation2 = new Reservation()
                {
                    Id = 2,
                    accommodation = accommodation,
                    CheckIn = DateTime.Today.AddDays(2),
                    CheckOut = DateTime.Today.AddDays(10),
                    Adults = new Tuple<int, int>(2, 1),
                    BabyQuantity = 0,
                    Total = 90000,
                    Name = "Martin",
                    Surname = "Gutman",
                    Email = "martin.gut",
                    ReservationState = ReservationState.Accepted,
                };

                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation, reservation2 });

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().accommodation, accommodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 2);
            }

            [TestMethod]
            public void ReportWithTwoaccommodationWithSameQuantity()
            {
                accommodation accommodation2 = new accommodation()
                {
                    Name = "Playa",
                    Stars = 4.0,
                    Address = "Cuareim",
                    Images = new List<Image> { new Image { Name = "imagen" } },
                    Fee = 4000,
                    Description = "Playa in Mvdeo",
                    Telephone = "+598",
                    ContactInformation = "Owner",
                    TouristSpot = touristSpot
                };

                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accommodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodation);
                accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<accommodation> { accommodation, accommodation2 });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                var result = reportHandler.accommodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10));
                accommodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 2);
                Assert.AreEqual(result.First().accommodation, accommodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
                Assert.AreEqual(result.Last().accommodation, accommodation2);
            }
        }
    }
}
