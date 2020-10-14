using BusinessLogic;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicTest
{
    [TestClass]
    public class CategoryHandlerTest
    {
        private Category categoryCiudad;
        private Category categoryPlaya;

        [TestInitialize]
        public void StartUp()
        {
            categoryCiudad = new Category { Name = "Ciudad" };
            categoryPlaya = new Category { Name = "Playa" };
        }

        [TestMethod]
        public void GetAllRegions()
        {
            var mock = new Mock<IRepository<Category>>(MockBehavior.Strict);
            var handler = new CategoryHandler(mock.Object);

            mock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).Returns(new List<Category> { categoryCiudad, categoryPlaya });

            List<Category> res = handler.GetAll();

            mock.VerifyAll();
            Assert.AreEqual(categoryCiudad, res[0]);
            Assert.AreEqual(categoryPlaya, res[1]);
            Assert.AreEqual(2, res.Count);
        }
    }
}
