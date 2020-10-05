using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class AdministratorControllerTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void PostAdminOk()
        {
            var mock = new Mock<IAdministratorHandler>(MockBehavior.Strict);
            var controller = new AdministratorController(mock.Object);
            var adminModel = new AdministratorModel()
            {
                Name = "Prueba",
                Email = "prueba@probando.com",
                Password = "12345678",
            };

            mock.Setup(x => x.Add(It.IsAny<Administrator>())).Returns(true);

            var result = controller.Post(adminModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

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
    }
}