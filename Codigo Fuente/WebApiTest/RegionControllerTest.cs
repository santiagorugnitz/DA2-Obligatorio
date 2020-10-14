using BusinessLogicInterface;
using Domain;
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
    public class RegionControllerTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void GetRegions()
        {
            var mock = new Mock<IRegionHandler>(MockBehavior.Strict);
            var controller = new RegionController(mock.Object);

            mock.Setup(x => x.GetAll()).Returns(new List<Region>() { new Region() { Name =  "Region Centro Sur" } });

            var result = controller.Get();
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<Region>;

            mock.VerifyAll();
        }
    }
}