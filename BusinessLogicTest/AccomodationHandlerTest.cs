using BusinessLogic;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicTest
{
    [TestClass]
    public class AccomodationHandlerTest
    {
        private Accomodation accomodation;
        private TouristSpot touristSpot;
        private Mock<IRepository<Accomodation>> accomodationMock;
        private Mock<IRepository<Region>> regionMock;
        private Mock<IRepository<Category>> categoryMock;
        private Mock<IRepository<Image>> imageMock;
        private Mock<IRepository<TouristSpot>> touristSpotMock;
        private Mock<IRepository<TouristSpotCategory>> joinedMock;
        private TouristSpotHandler touristSpotHandler;
        private AccomodationHandler handler;
        private List<string> imageNames;

    [TestInitialize]
        public void SetUp()
        {

            TouristSpotCategory joinedEntry = new TouristSpotCategory()
            {
                Category = new Category
                {
                    Name = "Ciudades"
                }
            };

            touristSpot = new TouristSpot
            {
                Name = "Beach",
                Description = "asd",
                Image = new Image { Name = "imagen" },
                Region = new Region() { Name =  "Region Centro Sur" },
                TouristSpotCategories = new List<TouristSpotCategory> { joinedEntry }
            };

            accomodation = new Accomodation()
            {
                Id = 1,
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                Available = true,
                Images = new List<Image> { new Image { Name = "imagen"} },
                Fee = 4000,
                Description = "Hotel in Mvdeo",
                Telephone = "+598",
                ContactInformation = "Owner",
                TouristSpot = touristSpot
            };

            imageNames = new List<string> { "imagen" };

            accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            regionMock = new Mock<IRepository<Region>>(MockBehavior.Loose);
            categoryMock = new Mock<IRepository<Category>>(MockBehavior.Loose);
            imageMock = new Mock<IRepository<Image>>(MockBehavior.Loose);
            touristSpotMock = new Mock<IRepository<TouristSpot>>(MockBehavior.Strict);
            joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            touristSpotHandler = new TouristSpotHandler(touristSpotMock.Object, imageMock.Object,
                categoryMock.Object, regionMock.Object, joinedMock.Object);
            handler = new AccomodationHandler(accomodationMock.Object, imageMock.Object, touristSpotHandler);
        }


        [TestMethod]
        public void AddAccomodationWithTouristSpot()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(accomodation.TouristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);
            imageMock.Setup(x => x.Add(accomodation.Images.ToList().First())).Returns(accomodation.Images.ToList().First());

            var res = handler.Add(accomodation, touristSpot.Id, imageNames);

            accomodationMock.VerifyAll();
            touristSpotMock.VerifyAll();
            Assert.AreEqual(accomodation, res);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The tourist spot does not exists")]
        public void AddAccomodationWithoutTouristSpot()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns((TouristSpot)null);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Accomodation needs a non empty name")]
        public void AddAccomodationWithoutName()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Name = "";
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Accomodation needs a non empty name")]
        public void AddAccomodationWithoutName2()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Name = "   ";
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Accomodation needs a non empty address")]
        public void AddAccomodationWithoutDirection()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Address = "";
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Accomodation needs a non empty address")]
        public void AddAccomodationWithoutDirection2()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Address = "   ";
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation stars needs to be between 1 and 5")]
        public void AddAccomodationWithNegativeStars()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Stars = -1;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation stars needs to be between 1 and 5")]
        public void AddAccomodationWithMoreThan5Stars()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Stars = 5.1;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation stars needs to be between 1 and 5")]
        public void AddAccomodationWith0Stars()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Stars = 0;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation fee needs to be more than 0")]
        public void AddAccomodationWith0Fee()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Fee = 0;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
    "The Accomodation fee needs to be more than 0")]
        public void AddAccomodationWithNegativeFee()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Fee = -10;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException),
    "The Accomodation needs at least one image")]
        public void AddAccomodationWithoutImages()
        {
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Images = new List<Image>();
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        public void DeleteAccomodation()
        {
            accomodationMock.Setup(x => x.Delete(accomodation)).Returns(true);
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);
            var res = handler.Delete(accomodation.Id);

            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void DeleteAccomodationWrongId()
        {
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns((Accomodation)null);

            var res = handler.Delete(accomodation.Id);

        }

        [TestMethod]
        public void ExistsAccomodation()
        {
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);

            var res = handler.Exists(accomodation);

            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void SearchAvailableAccomodation()
        {
            accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accomodation> { accomodation });

            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);
         
            var res = handler.SearchByTouristSpot(touristSpot.Id);

            accomodationMock.VerifyAll();
            Assert.AreEqual(new List<Accomodation> { accomodation }[0], res[0]);
        }

        [TestMethod]
        public void SearchNonAvailableAccomodation()
        {
            accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accomodation> { });

            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);

            var res = handler.SearchByTouristSpot(touristSpot.Id);

            accomodationMock.VerifyAll();
            Assert.AreEqual(0, res.Count);
        }

        [TestMethod]
        public void ChangeAccomodationAvaliabilityOk()
        {
            accomodationMock.Setup(x => x.Update(accomodation)).Returns(true);
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);

            var res = handler.ChangeAvailability(accomodation.Id, false);

            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void ChangeAccomodationAvaliabilityBadAccomodation()
        {
            accomodationMock.Setup(x => x.Update(accomodation)).Returns(true);
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns((Accomodation) null);

            var res = handler.ChangeAvailability(accomodation.Id, false);

            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void GetAccomodationFalse()
        {
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns((Accomodation) null);

            var res = handler.Get(accomodation.Id);

            accomodationMock.VerifyAll();
            Assert.AreEqual(null, res);
        }

        [TestMethod]
        public void GetAccomodationTrue()
        {
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns(accomodation);

            var res = handler.Get(accomodation.Id);

            accomodationMock.VerifyAll();
            Assert.AreEqual(accomodation, res);
        }
    }
}
