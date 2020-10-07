﻿using BusinessLogicInterface;
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
        public void PostAccomodationOk()
        {
            var mock = new Mock<IAccomodationHandler>(MockBehavior.Strict);
            var controller = new AccomodationController(mock.Object);
            var accomodationModel = new AccomodationModel()
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
                accomodationModel.TouristSpotId, accomodationModel.ImageNames)).Returns(true);

            var result = controller.Post(accomodationModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void ChangeAvailabilityOk()
        {
            var mock = new Mock<IAccomodationHandler>(MockBehavior.Strict);
            var controller = new AccomodationController(mock.Object);
            var accomodationModel = new AccomodationModel()
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

            mock.Setup(x => x.ChangeAvailability(It.IsAny<Accomodation>(),
                It.IsAny<bool>())).Returns(true);

            var result = controller.ChangeAvailability(accomodationModel, true);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void DeleteAccomodationOk()
        {
            var mock = new Mock<IAccomodationHandler>(MockBehavior.Strict);
            var controller = new AccomodationController(mock.Object);
            var accomodationModel = new AccomodationModel()
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

            mock.Setup(x => x.Delete(It.IsAny<Accomodation>())).Returns(true);

            var result = controller.Delete(accomodationModel);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAccomodationByIdOk()
        {
            var mock = new Mock<IAccomodationHandler>(MockBehavior.Strict);
            var controller = new AccomodationController(mock.Object);
            var accomodationModel = new AccomodationModel()
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

            mock.Setup(x => x.Get(It.IsAny<int>())).Returns(accomodationModel.ToEntity());

            var result = controller.Get(1);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAccomodationByTouristSpotOk()
        {
            var mock = new Mock<IAccomodationHandler>(MockBehavior.Strict);
            var controller = new AccomodationController(mock.Object);
            var accomodationModel = new AccomodationModel()
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

            mock.Setup(x => x.SearchByTouristSpot(It.IsAny<TouristSpot>(),
                It.IsAny<DateTime>(), It.IsAny<DateTime>())).
                Returns(new List<Accomodation> { accomodationModel.ToEntity() });

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

            var result = controller.GetByTouristSpot(touristSpotModel, DateTime.Now, DateTime.Now.AddMinutes(15));
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }
    }
}
