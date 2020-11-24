using BusinessLogicInterface;
using Domain;
using Exceptions;
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
    public class accommodationControllerTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void PostaccommodationOk()
        {
            var mock = new Mock<IaccommodationHandler>(MockBehavior.Strict);
            var controller = new accommodationController(mock.Object);
            var accommodationModel = new accommodationModel()
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

            mock.Setup(x => x.Add(It.IsAny<accommodation>(),
                accommodationModel.TouristSpotId, accommodationModel.ImageNames)).Returns(It.IsAny<accommodation>());

            var result = controller.Post(accommodationModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void ChangeAvailabilityOk()
        {
            var mock = new Mock<IaccommodationHandler>(MockBehavior.Strict);
            var controller = new accommodationController(mock.Object);
            var accommodationModel = new accommodationModel()
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

            mock.Setup(x => x.ChangeAvailability(1,
                It.IsAny<bool>())).Returns(true);

            var result = controller.ChangeAvailability(1, true);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void DeleteaccommodationOk()
        {
            var mock = new Mock<IaccommodationHandler>(MockBehavior.Strict);
            var controller = new accommodationController(mock.Object);
            var accommodationModel = new accommodationModel()
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

            mock.Setup(x => x.Delete(1)).Returns(true);

            var result = controller.Delete(1);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetaccommodationByIdOk()
        {
            var mock = new Mock<IaccommodationHandler>(MockBehavior.Strict);
            var controller = new accommodationController(mock.Object);
            var accommodationModel = new accommodationModel()
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

            mock.Setup(x => x.Get(It.IsAny<int>())).Returns(accommodationModel.ToEntity());

            var result = controller.Get(1);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetaccommodationByTouristSpotOk()
        {
            var mock = new Mock<IaccommodationHandler>(MockBehavior.Strict);
            var controller = new accommodationController(mock.Object);
            var accommodationModel = new accommodationModel()
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

            mock.Setup(x => x.SearchByTouristSpot(1,true)).
                Returns(new List<accommodation> { accommodationModel.ToEntity() });

            Image image = new Image()
            {
                Id = 1,
                Name = "image"
            };
            Category category = new Category
            {
                Id = 1,
                Name = "Campo"
            };
            Region region = new Region() { Name = "Region metropolitana" };
            TouristSpot spot = new TouristSpot()
            {
                Id = 1,
                Name = "Beach",
                Description = "asd",
                Image = image,
                Region = region,
                TouristSpotCategories = new List<TouristSpotCategory> { new TouristSpotCategory() { Category = category } }
            };
            TouristSpotModel touristSpotModel = new TouristSpotModel()
            {
                Name = spot.Name,
                Description = spot.Description,
                RegionId = region.Id,
                CategoryIds = new List<int>() { category.Id },
                Image = image.Name,

            };

            var result = controller.GetByTouristSpot(spot.Id,true);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Get404()
        {
            var mock = new Mock<IaccommodationHandler>(MockBehavior.Strict);
            var controller = new accommodationController(mock.Object);
            mock.Setup(x => x.Get(It.IsAny<int>())).Returns((accommodation)null);

            var result = controller.Get(1);
        }

        [TestMethod]
        public void CalculateTotal()
        {
            var mock = new Mock<IaccommodationHandler>(MockBehavior.Strict);
            var controller = new accommodationController(mock.Object);
            var stay = new StayModel()
            {
                AdultQuantity = 3,
                ChildrenQuantity = 2,
                BabyQuantity = 1,
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(10)
            };

            mock.Setup(x => x.CalculateTotal(1, It.IsAny<Stay>())).Returns(500.2);

            var result = controller.CalculateTotal(1,stay);
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
            Assert.IsTrue(okResult.Value is double);
        }

    }
}
