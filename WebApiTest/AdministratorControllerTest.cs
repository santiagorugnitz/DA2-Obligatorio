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
    }
}