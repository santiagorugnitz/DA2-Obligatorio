using BusinessLogic;
using DataAccessInterface;
using DataImport;
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
    public class DllHandlerTest
    {
        private DllHandler handler;

        [TestInitialize]
        public void SetUp()
        {
            handler = new DllHandler();
        }

        [TestMethod]
        public void GetDllsTest()
        {
            JsonImporter.JsonImporter json = new JsonImporter.JsonImporter();
            XmlImporter.XmlImporter xml = new XmlImporter.XmlImporter();

            List<IImporter> list = new List<IImporter> { json, xml };
            var dlls = handler.GetDlls();

            Assert.AreEqual(dlls.ToList()[0].GetType(), list[0].GetType());
            Assert.AreEqual(dlls.ToList()[1].GetType(), list[1].GetType());
            Assert.AreEqual(dlls.Count(), 2);
        }
    }
}