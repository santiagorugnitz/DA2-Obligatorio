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
    public class ReservationControllerTest
    {
        [TestMethod]
        public void PostReservationOk()
        {
            var mock = new Mock<IReservationHandler>(MockBehavior.Strict);
            var controller = new ReservationController(mock.Object);
            var reservationModel = new ReservationModel()
            {
                Name = "Prueba",
                AdultQuantity = 3,
                ChildrenQuantity = 2,
                BabyQuantity = 1,
                Email = "Email@prueba",
                Surname = "Prueba",
                ReservationState = ReservationState.Aceptada,
                StateDescription = "Done",
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(10)
            };

            mock.Setup(x => x.Add(It.IsAny<Reservation>(),
                reservationModel.AccomodationId)).Returns(reservationModel.ToEntity());

            var result = controller.Post(reservationModel);
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
        }

        [TestMethod]
        public void CheckReservationsStateOk()
        {
            var mock = new Mock<IReservationHandler>(MockBehavior.Strict);
            var controller = new ReservationController(mock.Object);
            var reservationModel = new ReservationModel()
            {
                Name = "Prueba",
                AdultQuantity = 3,
                ChildrenQuantity = 2,
                BabyQuantity = 1,
                Email = "Email@prueba",
                Surname = "Prueba",
                ReservationState = ReservationState.Aceptada,
                StateDescription = "Done",
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(10)
            };

            mock.Setup(x => x.CheckState(It.IsAny<int>())).Returns(reservationModel.ToEntity());

            var result = controller.CheckState(1);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void ChangeReservationsStateOk()
        {
            var mock = new Mock<IReservationHandler>(MockBehavior.Strict);
            var controller = new ReservationController(mock.Object);
            var reservationModel = new ReservationModel()
            {
                Name = "Prueba",
                AdultQuantity = 3,
                ChildrenQuantity = 2,
                BabyQuantity = 1,
                Email = "Email@prueba",
                Surname = "Prueba",
                ReservationState = ReservationState.Aceptada,
                StateDescription = "Done",
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(10)
            };

            ReservationChangeModel model = new ReservationChangeModel()
            {
                State = ReservationState.Aceptada,
                Description = "valid"
            };

            mock.Setup(x => x.ChangeState(It.IsAny<int>(), It.IsAny<ReservationState>()
                , It.IsAny<string>())).Returns(true);

            var result = controller.ChangeState(1, model);
            var okResult = result as OkObjectResult;
            var value = okResult.Value as bool?;

            mock.VerifyAll();
        }

        [TestMethod]
        public void ReviewOk()
        {
            var mock = new Mock<IReservationHandler>(MockBehavior.Strict);
            var controller = new ReservationController(mock.Object);
            var reservationModel = new ReservationModel()
            {
                Name = "Prueba",
                AdultQuantity = 3,
                ChildrenQuantity = 2,
                BabyQuantity = 1,
                Email = "Email@prueba",
                Surname = "Prueba",
                ReservationState = ReservationState.Aceptada,
                StateDescription = "Done",
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(10)
            };

            ReviewModel model = new ReviewModel()
            {
                Score = 2,
                Comment = "ok"
            };

            mock.Setup(x => x.Review(It.IsAny<int>(), model.Score
                , model.Comment)).Returns(true);

            var result = controller.Review(1, model);
            var okResult = result as OkObjectResult;

            mock.VerifyAll();
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void Get404()
        {
            var mock = new Mock<IReservationHandler>(MockBehavior.Strict);
            var controller = new ReservationController(mock.Object);
            mock.Setup(x => x.CheckState(It.IsAny<int>())).Returns((Reservation)null);

            var result = controller.CheckState(1);
        }

        [TestMethod]
        public void GetFromAccomodation()
        {
            var mock = new Mock<IReservationHandler>(MockBehavior.Strict);
            var controller = new ReservationController(mock.Object);
            var reservationModel = new ReservationModel()
            {
                Name = "Prueba",
                AdultQuantity = 3,
                ChildrenQuantity = 2,
                BabyQuantity = 1,
                Email = "Email@prueba",
                Surname = "Prueba",
                ReservationState = ReservationState.Aceptada,
                StateDescription = "Done",
                CheckIn = DateTime.Now,
                CheckOut = DateTime.Now.AddDays(10)
            };
            mock.Setup(x => x.GetAllFromAccomodation(1)).Returns(new List<Reservation>() { reservationModel.ToEntity()});

            var result = controller.GetFromAccomodation(1);

            Assert.AreEqual(true, result is OkObjectResult);
            mock.VerifyAll();
        }
    }
}
