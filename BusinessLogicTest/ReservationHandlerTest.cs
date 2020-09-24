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
        [TestMethod]
        public void AddReservation()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object);

            var reservation = new Reservation()
            {
            };
            mock.Setup(x => x.Add(reservation)).Returns(true);

            var res = handler.Add(reservation);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void DeleteReservation()
        {
            var mock = new Mock<IRepository<Reservation>>(MockBehavior.Strict);
            var handler = new ReservationHandler(mock.Object);

            var spot = new Reservation()
            {
            };
            mock.Setup(x => x.Delete(spot)).Returns(true);

            var res = handler.Delete(spot);

            mock.VerifyAll();
            Assert.AreEqual(true, res);

        }
    }
}
