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
            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>()))
                .Returns(new List<Administrator> { });

            var res = handler.Add(administrator);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Administrator needs a non empty name")]
        public void AddAdminWithoutName1()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(true);

            administrator.Name = "";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Administrator needs a non empty name")]
        public void AddAdminWithoutName2()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(true);

            administrator.Name = "    ";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
   "The Administrator needs a non empty email")]
        public void AddAdminWithoutEmail1()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(true);

            administrator.Email = "";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Administrator needs a non empty email")]
        public void AddAdminWithoutEmail2()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(true);

            administrator.Email = "    ";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),
   "The mail already exists")]
        public void AddAdminWithRepeatedEmail()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(true);

            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>()))
                .Returns(new List<Administrator> { });

            handler.Add(administrator);

            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>()))
                .Returns(new List<Administrator> { administrator });

            Administrator incorrectAdmin = new Administrator
            {
                Name = "Martin",
                Email = "santi.rug",
                Password = "1234"
            };

            var res = handler.Add(incorrectAdmin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
   "The Administrator needs a non empty password")]
        public void AddAdminWithoutPassword1()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(true);

            administrator.Password = "";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Administrator needs a non empty password")]
        public void AddAdminWithoutPassword2()
        {
            var mock = new Mock<IRepository<Administrator>>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(true);

            administrator.Password = "    ";
            var res = handler.Add(administrator);
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
