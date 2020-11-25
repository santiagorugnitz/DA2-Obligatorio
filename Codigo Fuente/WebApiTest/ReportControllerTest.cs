using BusinessLogicInterface;
using Domain;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class ReportControllerTest
    {
        [TestMethod]
        public void GetReport()
        {
            var mock = new Mock<IReportHandler>(MockBehavior.Strict);
            var controller = new ReportController(mock.Object);

            mock.Setup(x => x.accommodationsReport(1, It.IsAny<DateTime>(), It.IsAny<DateTime>())).
                Returns(new List<ReportItem>() { new ReportItem() { accommodation = new Accommodation(), ReservationsQuantity = 1 } });

            var result = controller.Get(1, DateTime.Now, DateTime.Now);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as List<ReportItem>;

            mock.VerifyAll();
        }
    }
}
