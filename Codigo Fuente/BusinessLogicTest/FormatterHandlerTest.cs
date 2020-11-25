﻿using BusinessLogic;
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
using DataImport;


namespace BusinessLogicTest
{
    [TestClass]
    public class FormatterHandlerTest
    {
        public static accommodationImport accommodation = new accommodationImport
        {
            Name = "accommodation"
        };

        private class FormatterMock : IImporter
        {
            public string GetName()
            {
                return "json";
            }

            List<accommodationImport> IImporter.Upload(List<SourceParameter> sourceParameters)
            {
                return new List<accommodationImport> { accommodation };
            }
        }

        private Mock<IDllHandler> dllMock;
        private ImporterHandler handler;
        private Mock<IaccommodationHandler> accommodationMock;

        [TestInitialize]
        public void SetUp()
        {
            dllMock = new Mock<IDllHandler>(MockBehavior.Strict);
            dllMock.Setup(x => x.GetDlls()).Returns(new List<IImporter> { new FormatterMock()});

            accommodationMock = new Mock<IaccommodationHandler>(MockBehavior.Strict);
            accommodationMock.Setup(x => x.Add(new List<accommodationImport> { accommodation })).Returns(true);

            handler = new ImporterHandler(dllMock.Object, accommodationMock.Object);
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