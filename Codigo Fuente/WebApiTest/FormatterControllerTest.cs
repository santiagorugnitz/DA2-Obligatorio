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
    public class FormatterControllerTest
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void GetFormatters()
        {
            var mock = new Mock<IFormatterHandler>(MockBehavior.Strict);
            var controller = new FormatterController(mock.Object);

            mock.Setup(x => x.GetAll()).Returns(new List<string>() { "json","xml" });

            var result = controller.Get();
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<string>;

            mock.VerifyAll();
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void GetFiles()
        {
            var mock = new Mock<IFormatterHandler>(MockBehavior.Strict);
            var controller = new FormatterController(mock.Object);

            mock.Setup(x => x.GetFileNames(1)).Returns(new List<string>() { "respaldo.json", "colonia.json" });

            var result = controller.GetFiles(1);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<string>;

            mock.VerifyAll();
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void LoadFile()
        {
            var mock = new Mock<IFormatterHandler>(MockBehavior.Strict);
            var controller = new FormatterController(mock.Object);

            mock.Setup(x => x.Add(1,"archivo")).Returns(true);

            var result = controller.LoadFile(1,"archivo");
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
            Assert.IsNotNull(okResult);
        }
    }
}