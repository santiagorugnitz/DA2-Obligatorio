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

            private Accomodation accomodation;
            private TouristSpot touristSpot;
            private Reservation reservation;
            private Mock<IRepository<Accomodation>> accomodationMock;
            private Mock<IRepository<Region>> regionMock;
            private Mock<IRepository<Category>> categoryMock;
            private Mock<IRepository<Image>> imageMock;
            private Mock<IRepository<TouristSpot>> touristSpotMock;
            private Mock<IRepository<TouristSpotCategory>> joinedMock;
            private Mock<IRepository<Reservation>> reservationMock;
            private TouristSpotHandler touristSpotHandler;
            private AccomodationHandler accomodationHandler;
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
                    Adults = new Tuple<int, int>(2,1),
                    BabyQuantity = 0,
                    Total = 90000,
                    Name = "Martin",
                    Surname = "Gutman",
                    Email = "martin.gut",
                    ReservationState = ReservationState.Aceptada,
                };

                accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Strict);
                regionMock = new Mock<IRepository<Region>>(MockBehavior.Loose);
                categoryMock = new Mock<IRepository<Category>>(MockBehavior.Loose);
                imageMock = new Mock<IRepository<Image>>(MockBehavior.Loose);
                touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
                joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
                reservationMock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
                touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object,
                    categoryMock.Object, regionMock.Object, joinedMock.Object);
                accomodationHandler = new AccomodationHandler(accomodationMock.Object, touristSpotHandler);
                reservationHandler = new ReservationHandler(reservationMock.Object, accomodationHandler);
                reportHandler = new ReportHandler(accomodationHandler, reservationHandler);
            }

            [TestMethod]
            [ExpectedException(typeof(BadRequestException),
    "The starting date must be before the finishing one")]
            public void ReportWithIncorrectDates()
            {
                reportHandler.AccomodationsReport(1, DateTime.Now, DateTime.Now.AddDays(-1));
            }

            [TestMethod]
            public void ReportWithoutAccomodations()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation>());

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-1));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithReservationWithNonIncludedDate1()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now;
                reservation.CheckOut = DateTime.Now.AddDays(1);

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithReservationWithNonIncludedDate2()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now.AddDays(4);
                reservation.CheckOut = DateTime.Now.AddDays(5);

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithReservationWithIncludedDate1()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now;
                reservation.CheckOut = DateTime.Now.AddDays(2);

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().Accomodation, accomodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithReservationWithIncludedDate2()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now.AddDays(2);
                reservation.CheckOut = DateTime.Now.AddDays(4);

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(3));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().Accomodation, accomodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithReservationWithIncludedDate3()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now.AddDays(2);
                reservation.CheckOut = DateTime.Now.AddDays(4);

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().Accomodation, accomodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithReservationWithIncludedDate4()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.CheckIn = DateTime.Now;
                reservation.CheckOut = DateTime.Now.AddDays(1);

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().Accomodation, accomodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithReservationWithNonIncludedState1()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.ReservationState = ReservationState.Rechazada;

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-1));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithReservationWithNonIncludedState2()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                reservation.ReservationState = ReservationState.Expirada;

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(-1));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 0);
            }

            [TestMethod]
            public void ReportWithOneReservationForOneAccomodation()
            {
                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().Accomodation, accomodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
            }

            [TestMethod]
            public void ReportWithTwoReservationForOneAccomodation()
            {
                Reservation reservation2 = new Reservation()
                {
                    Id = 2,
                    Accomodation = accomodation,
                    CheckIn = DateTime.Today.AddDays(2),
                    CheckOut = DateTime.Today.AddDays(10),
                    Adults = new Tuple<int, int>(2, 1),
                    BabyQuantity = 0,
                    Total = 90000,
                    Name = "Martin",
                    Surname = "Gutman",
                    Email = "martin.gut",
                    ReservationState = ReservationState.Aceptada,
                };

                touristSpotMock.Setup(x => x.Get(1)).Returns(touristSpot);
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation, reservation2 });

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.First().Accomodation, accomodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 2);
            }

            [TestMethod]
            public void ReportWithTwoAccomodationWithSameQuantity()
            {
                Accomodation accomodation2 = new Accomodation()
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
                accomodationMock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodation);
                accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Accomodation> { accomodation, accomodation2 });
                reservationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                    Returns(new List<Reservation> { reservation });

                var result = reportHandler.AccomodationsReport(1, DateTime.Now.AddDays(-10), DateTime.Now.AddDays(10));
                accomodationMock.VerifyAll();
                Assert.AreEqual(result.Count(), 2);
                Assert.AreEqual(result.First().Accomodation, accomodation);
                Assert.AreEqual(result.First().ReservationsQuantity, 1);
                Assert.AreEqual(result.Last().Accomodation, accomodation2);
            }
        }
    }
}
