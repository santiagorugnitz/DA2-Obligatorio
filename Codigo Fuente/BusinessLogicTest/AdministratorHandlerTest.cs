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
                Password = "1234",
                Token = "asdg25-f812j"
            };
        }

        [TestMethod]
        public void AddAdmin()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(administrator);
            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>()))
                .Returns(new List<Administrator> { });

            var res = handler.Add(administrator);

            mock.VerifyAll();
            Assert.AreEqual(administrator, res);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Administrator needs a non empty name")]
        public void AddAdminWithoutName1()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(administrator);

            administrator.Name = "";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Administrator needs a non empty name")]
        public void AddAdminWithoutName2()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(administrator);

            administrator.Name = "    ";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
   "The Administrator needs a non empty email")]
        public void AddAdminWithoutEmail1()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(administrator);

            administrator.Email = "";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Administrator needs a non empty email")]
        public void AddAdminWithoutEmail2()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(administrator);

            administrator.Email = "    ";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
   "The mail already exists")]
        public void AddAdminWithRepeatedEmail()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(administrator);

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
        [ExpectedException(typeof(BadRequestException),
   "The Administrator needs a non empty password")]
        public void AddAdminWithoutPassword1()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(administrator);

            administrator.Password = "";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Administrator needs a non empty password")]
        public void AddAdminWithoutPassword2()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Add(administrator)).Returns(administrator);

            administrator.Password = "    ";
            var res = handler.Add(administrator);
        }

        [TestMethod]
        public void DeleteAdmin()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Delete(administrator)).Returns(true);
            mock.Setup(x => x.Get(administrator.Id)).Returns(administrator);


            var res = handler.Delete(administrator.Id);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void DeleteAdminWrongId()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Get(administrator.Id)).Returns((Administrator)null);

            var res = handler.Delete(administrator.Id);

        }

        [TestMethod]
        public void LoginOk()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Find(administrator.Email, administrator.Password)).Returns(administrator);
            mock.Setup(x => x.Update(administrator)).Returns(true);


            var res = handler.Login(administrator.Email, administrator.Password);

            mock.VerifyAll();
            Assert.AreNotEqual(null, res);
        }

        [ExpectedException(typeof(BadRequestException),"The Administrator needs a non empty password")]
        [TestMethod]
        public void LoginBad()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Find(administrator.Email, administrator.Password)).Returns((Administrator)null);
            mock.Setup(x => x.Update(administrator)).Returns(true);

            var res = handler.Login(administrator.Email, administrator.Password);

            mock.VerifyAll();
        }

        [TestMethod]
        public void LogoutOk()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Find(administrator.Token)).Returns(administrator);
            mock.Setup(x => x.Update(administrator)).Returns(true);

            handler.Logout(administrator.Token);

            mock.VerifyAll();
        }

        [TestMethod]
        public void LogoutBad()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Find(administrator.Token)).Returns((Administrator)null);

            handler.Logout(administrator.Token);

            mock.VerifyAll();
        }

        [TestMethod]
        public void IsLoggedTrue()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Find(administrator.Token)).Returns((Administrator)null);

            var res = handler.IsLogged(administrator.Token);

            mock.VerifyAll();
            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void IsLoggedFalse()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Find(administrator.Token)).Returns(administrator);
            
            var res = handler.IsLogged(administrator.Token);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void Get()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Get(administrator.Id)).Returns(administrator);

            var res = handler.Get(administrator.Id);

            mock.VerifyAll();
            Assert.AreEqual(administrator, res);
        }

        [TestMethod]
        public void GetAll()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.GetAll(null)).Returns(new List<Administrator>() { administrator });

            var res = handler.GetAll();

            mock.VerifyAll();
            Assert.AreEqual(administrator, res[0]);
        }
        [TestMethod]
        public void Update()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);
            List<Administrator> returnedList = new List<Administrator>();
            returnedList.Add(administrator);

            mock.Setup(x => x.Get(administrator.Id)).Returns(administrator);
            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).Returns(returnedList);
            mock.Setup(x => x.Update(administrator)).Returns(true);

            var res = handler.Update(administrator);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [ExpectedException(typeof(NotFoundException), "There is no administrator with that id")]
        [TestMethod]
        public void UpdateUnsuccesful()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);
            List<Administrator> returnedList = new List<Administrator>();
            returnedList.Add(administrator);

            mock.Setup(x => x.Get(administrator.Id)).Returns((Administrator) null);
            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).Returns(returnedList);
            mock.Setup(x => x.Update(administrator)).Returns(true);

            var res = handler.Update(administrator);

            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [ExpectedException(typeof(BadRequestException))]
        [TestMethod]
        public void UpdateUnsuccesfullByRepeatedEmail()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);
            
            List<Administrator> returnedList = new List<Administrator>();
            returnedList.Add(administrator);
            var admin2 = new Administrator();
            admin2 = administrator;
            returnedList.Add(admin2);

            mock.Setup(x => x.Get(administrator.Id)).Returns(administrator);
            mock.Setup(x => x.GetAll(It.IsAny<Func<object,bool>>())).Returns(returnedList);
            //mock.Setup(x => x.Update(administrator)).Returns(true);

            var res = handler.Update(administrator);

        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void DeleteWrongId()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Get(administrator.Id)).Returns((Administrator)null);

            var res = handler.Delete(administrator.Id);
        }

        [TestMethod]
        public void DeleteSuccesfull()
        {
            var mock = new Mock<IAdministratorRepository>(MockBehavior.Strict);
            var handler = new AdministratorHandler(mock.Object);

            mock.Setup(x => x.Get(administrator.Id)).Returns(administrator);
            mock.Setup(x => x.Delete(administrator)).Returns(true);

            var res = handler.Delete(administrator.Id);
            mock.VerifyAll();
            Assert.AreEqual(true, res);
        }


    }
}
