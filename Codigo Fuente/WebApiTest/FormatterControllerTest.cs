using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using WebApi.Controllers;
using WebApi.Models;
using DataImport;

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
            var mock = new Mock<IImporterHandler>(MockBehavior.Strict);
            var controller = new ImporterController(mock.Object);

            mock.Setup(x => x.GetAll()).Returns(new List<string>() { "json","xml" });

            var result = controller.Get();
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<string>;

            mock.VerifyAll();
            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void LoadFile()
        {
            var mock = new Mock<IImporterHandler>(MockBehavior.Strict);
            var controller = new ImporterController(mock.Object);
            
            List<SourceParameter> parameters = new List<SourceParameter> { new SourceParameter
            { Type = ParameterType.String, Name = "archivo" } };

            mock.Setup(x => x.Add(1, parameters)).Returns(true);

            var result = controller.Upload(1,parameters);
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public void LoadFileFailed()
        {
            var mock = new Mock<IImporterHandler>(MockBehavior.Strict);
            var controller = new ImporterController(mock.Object);

            List<SourceParameter> parameters = new List<SourceParameter> { new SourceParameter
            { Type = ParameterType.String, Name = "archivo" } };

            mock.Setup(x => x.Add(1, parameters)).Returns(false);

            var result = controller.Upload(1, parameters);
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
            Assert.IsNotNull(okResult);
        }
    }
}