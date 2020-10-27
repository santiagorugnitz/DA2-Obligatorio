using BusinessLogic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicTest
{
    [TestClass]
    public class FormatterHandlerTest
    {
        private class FormatterMock : IFormatter
        {
            public string GetName()
            {
                return "json";
            }

            public bool UploadFromFile(string fileName)
            {
                return true;
            }
        }

        private class FormatterMock2 : IFormatter
        {
            public string GetName()
            {
                return "json";
            }

            public bool UploadFromFile(string fileName)
            {
                return false;
            }
        }

        private Mock<IDllHandler> dllMock;
        private FormatterHandler handler;

        [TestInitialize]
        public void SetUp()
        {
            dllMock = new Mock<IDllHandler>(MockBehavior.Strict);
            dllMock.Setup(x => x.GetDlls()).Returns(new List<IFormatter> { new FormatterMock()});

            handler = new FormatterHandler(dllMock.Object);
        }

        [TestMethod]
        public void AddFile()
        {
            var result = handler.Add(0, "test.json");

            dllMock.VerifyAll();
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void AddFileWithBadPosition1()
        {
            var result = handler.Add(1, "test.json");
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void AddFileWithBadPosition2()
        {
            var result = handler.Add(-1, "test");
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void AddFileWithBadName1()
        {
            dllMock.Setup(x => x.GetDlls()).Returns(new List<IFormatter> { new FormatterMock2() });

            var result = handler.Add(0, "test.exe");
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void AddFileWithBadName2()
        {
            dllMock.Setup(x => x.GetDlls()).Returns(new List<IFormatter> { new FormatterMock2() });

            var result = handler.Add(0, "");
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void AddUnexistingFile()
        {
            dllMock.Setup(x => x.GetDlls()).Returns(new List<IFormatter> { new FormatterMock2() });

            var result = handler.Add(0, "test.json");
        }

        [TestMethod]
        public void GetNames()
        {
            var result = handler.GetAll();

            dllMock.VerifyAll();
            Assert.AreEqual(result.First(), "json");
            Assert.AreEqual(result.Count, 1);
        }
    }
}
