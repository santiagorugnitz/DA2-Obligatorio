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
        public static Accomodation accomodation = new Accomodation
        {
            Name = "Accomodation"
        };

        private class FormatterMock : IFormatter
        {
            public string GetName()
            {
                return "json";
            }

            List<Accomodation> IFormatter.Upload(List<SourceParameter> sourceParameters)
            {
                return new List<Accomodation> { accomodation };
            }
        }

        private Mock<IDllHandler> dllMock;
        private FormatterHandler handler;
        private Mock<IAccomodationHandler> accomodationMock;

        [TestInitialize]
        public void SetUp()
        {
            dllMock = new Mock<IDllHandler>(MockBehavior.Strict);
            dllMock.Setup(x => x.GetDlls()).Returns(new List<IFormatter> { new FormatterMock()});

            accomodationMock = new Mock<IAccomodationHandler>(MockBehavior.Strict);
            accomodationMock.Setup(x => x.Add(new List<Accomodation> { accomodation })).Returns(true);

            handler = new FormatterHandler(dllMock.Object, accomodationMock.Object);
        }

        [TestMethod]
        public void AddFile()
        {
            var result = handler.Add(0, new List<SourceParameter> { new SourceParameter 
            { Type = ParameterType.String, Name = "test.json" } });

            dllMock.VerifyAll();
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void AddFileWithBadPosition1()
        {
            var result = handler.Add(1, new List<SourceParameter> { new SourceParameter
            { Type = ParameterType.String, Name = "test.json" } });
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public void AddFileWithBadPosition2()
        {
            var result = handler.Add(-1, new List<SourceParameter> { new SourceParameter
            { Type = ParameterType.String, Name = "test.json" } });
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
