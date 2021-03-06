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
    public class TouristSpotControllerTest
    {
        private TouristSpot spot;
        private Image image;
        private Category category;
        private Region region;
        private TouristSpotModel spotModel;

        [TestInitialize]
        public void SetUp()
        {
            image = new Image()
            {
                Id = 1,
                Name = "image"
            };
            category = new Category
            {
                Id = 1,
                Name = "Campo"
            };
            region = new Region() { Name = "Region metropolitana" };
            spot = new TouristSpot()
            {
                Id = 1,
                Name = "Beach",
                Description = "asd",
                Image = image,
                Region = region,
                TouristSpotCategories = new List<TouristSpotCategory> { new TouristSpotCategory() { Category = category } }
            };
            spotModel = new TouristSpotModel()
            {
                Name = spot.Name,
                Description = spot.Description,
                RegionId = region.Id,
                CategoryIds = new List<int>() { category.Id },
                Image = image.Name,

            };

        }

        [TestMethod]
        public void GetSpotByIdOk()
        {
            var mock = new Mock<ITouristSpotHandler>(MockBehavior.Strict);
            var controller = new TouristSpotController(mock.Object);

            mock.Setup(x => x.Get(spot.Id)).Returns(spot);

            var result = controller.Get(spot.Id);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as TouristSpot;

            mock.VerifyAll();
            Assert.AreEqual(spot, value);
        }

        [TestMethod]
        public void PostOk()
        {
            var mock = new Mock<ITouristSpotHandler>(MockBehavior.Strict);
            var controller = new TouristSpotController(mock.Object);

            mock.Setup(x => x.Add(spotModel.ToEntity(),spotModel.RegionId,spotModel.CategoryIds,spotModel.Image)).Returns(spotModel.ToEntity());

            var result = controller.Post(spotModel);
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
        }

        [TestMethod]
        public void GetAllOk()
        {
            var mock = new Mock<ITouristSpotHandler>(MockBehavior.Strict);
            var controller = new TouristSpotController(mock.Object);

            var categories = new List<int>() { category.Id };

            mock.Setup(x => x.Search(categories,region.Id)).Returns(new List<TouristSpot>() { spot});

            var result = controller.GetAll(region.Id,categories);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<TouristSpot>;

            mock.VerifyAll();
            Assert.AreEqual(spot, value[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Get404()
        {
            var mock = new Mock<ITouristSpotHandler>(MockBehavior.Strict);
            var controller = new TouristSpotController(mock.Object);
            mock.Setup(x => x.Get(It.IsAny<int>())).Returns((TouristSpot)null);

            var result = controller.Get(1);
        }
    }
}
