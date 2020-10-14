using BusinessLogic;
using BusinessLogicInterface;
using Domain;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class AdministratorControllerTest
    {

        AdministratorModel adminModel;
        Administrator admin;

        [TestInitialize]
        public void Setup()
        {
            adminModel = new AdministratorModel()
            {
                Name = "Prueba",
                Email = "prueba@probando.com",
                Password = "12345678",
            };
            admin = adminModel.ToEntity();
        }

        [TestMethod]
        public void PostAdminOk()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            var controller = new AdministratorController(mock.Object);
            
            mock.Setup(x => x.Add(It.IsAny<Administrator>())).Returns(It.IsAny<Administrator>());

            var result = controller.Post(adminModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAll()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            var controller = new AdministratorController(mock.Object);

            mock.Setup(x => x.GetAll()).Returns(new List<Administrator>() { admin});

            var result = controller.GetAll();
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<Administrator>;

            mock.VerifyAll();
        }

        [TestMethod]
        public void Get()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            var controller = new AdministratorController(mock.Object);

            mock.Setup(x => x.Get(admin.Id)).Returns(admin);

            var result = controller.Get(admin.Id);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as Administrator;

            mock.VerifyAll();
        }

        [TestMethod]
        public void Update()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            var controller = new AdministratorController(mock.Object);

            mock.Setup(x => x.Update(admin)).Returns(true);

            var result = controller.Update(admin.Id,adminModel);
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
        }

        [TestMethod]
        public void Delete()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            var controller = new AdministratorController(mock.Object);

            mock.Setup(x => x.Delete(admin.Id)).Returns(true);

            var result = controller.Delete(admin.Id);
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
        }


        [TestMethod]
        public void LoginOk()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            var controller = new AdministratorController(mock.Object);
            var loginModel = new LoginModel()
            {
                Email = "prueba@probando.com",
                Password = "12345678",
            };

            mock.Setup(x => x.Login(loginModel.Email,loginModel.Password)).Returns("token");

            var result = controller.Login(loginModel);
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
        }

        [TestMethod]
        public void LogoutOk()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            var controller = new AdministratorController(mock.Object);

            mock.Setup(x => x.Logout("token"));

            var result = controller.Logout("token");
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Get404()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            var controller = new AdministratorController(mock.Object);
            mock.Setup(x => x.Get(It.IsAny<int>())).Returns((Administrator)null);

            var result = controller.Get(1);
        }
    }
}