using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class AccomodationControllerTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void PostAdminOk()
        {
            var mock = new Mock<IAccomodationHandler>(MockBehavior.Strict);
            var controller = new AccomodationController(mock.Object);
            var adminModel = new AccomodationModel()
            {
                Name = "Prueba",
                Stars = 3.0,
                Address = "Cuareim",
                ImageNames = new List<string> { "image" },
                Fee = 200,
                Description = "",
                Available = true,
                Telephone = "00000",
                ContactInformation = "",
                TouristSpotId = 1
            };

            mock.Setup(x => x.Add(It.IsAny<Accomodation>(),
                adminModel.TouristSpotId, adminModel.ImageNames)).Returns(true);

            var result = controller.Post(adminModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();

        }
    }
}
