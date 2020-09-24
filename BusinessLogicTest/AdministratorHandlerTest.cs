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
        private Administrator administrator;

        [TestInitialize]
        public void SetUp()
        {
            administrator = new Administrator
            {
                Name = "Santiago",
                Email = "santi.rug",
                Password = "1234"
            };
        }

        [TestMethod]
        public void AddAdmin()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(true);

            var res = handler.Add(administrator);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void DeleteAdmin()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Delete(administrator)).Returns(true);

            var res = handler.Delete(administrator);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }
    }
}
