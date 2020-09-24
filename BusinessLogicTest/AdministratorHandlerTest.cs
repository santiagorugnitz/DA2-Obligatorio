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
    public class AdministratorHandlerTest
    {
        [TestMethod]
        public void AddAdmin()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            var spot = new Administrator()
            {
            };
            mock.Setup(x => x.Add(spot)).Returns(true);

            var res = handler.Add(spot);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void DeleteAdmin()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            var spot = new Administrator()
            {
            };
            mock.Setup(x => x.Delete(spot)).Returns(true);

            var res = handler.Delete(spot);

            mock.VerifyAll();
            Assert.AreEqual(true, res);

        }
    }
}
