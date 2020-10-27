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
                return "JSON";
            }

            public bool UploadFromFile(string fileName)
            {
                return true;
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
            var result = handler.Add(0, "test");

            dllMock.VerifyAll();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetNames()
        {
            var result = handler.GetAll();

            dllMock.VerifyAll();
            Assert.AreEqual(result.First(), "JSON");
            Assert.AreEqual(result.Count, 1);
        }
    }
}
