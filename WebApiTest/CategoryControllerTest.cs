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
    public class CategoryControllerTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void GetCategories()
        {
            var mock = new Mock<ICategoryHandler>(MockBehavior.Strict);
            var controller = new CategoryController(mock.Object);

            mock.Setup(x => x.GetAll()).Returns(new List<Category>() { new Category() { Name="City"} });

            var result = controller.Get();
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<Category>;

            mock.VerifyAll();
        }
    }
}