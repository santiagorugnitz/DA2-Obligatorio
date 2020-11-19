using BusinessLogic;
using BusinessLogicInterface;
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
    public class AccomodationHandlerTest
    {
        private Accomodation accomodation;
        private TouristSpot touristSpot;
        private Mock<IRepository<Accomodation>> accomodationMock;
        private Mock<IRepository<Region>> regionMock;
        private Mock<IRepository<Category>> categoryMock;
        private Mock<IRepository<TouristSpotCategory>> joinedMock;
        private Mock<ITouristSpotHandler> touristSpotHandlerMock;
        private AccomodationHandler handler;
        private List<string> imageNames;
        private List<AccomodationImport> import;

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
                Region = new Region() { Name = "Region Centro Sur" },
                TouristSpotCategories = new List<TouristSpotCategory> { joinedEntry }
            };

            accomodation = new Accomodation()
            {
                Id = 1,
                Name = "Hotel",
                Stars = 4.0,
                Address = "Cuareim",
                Available = true,
                Images = new List<Image> { new Image { Name = "imagen" } },
                Fee = 4000,
                Description = "Hotel in Mvdeo",
                Telephone = "+598",
                ContactInformation = "Owner",
                TouristSpot = touristSpot
            };

            imageNames = new List<string> { "imagen" };

            import = new List<AccomodationImport>() {
                new AccomodationImport
                {
                    Name = "Hotel",
                    Stars = 4.0,
                    Address = "Cuareim",
                    Available = true,
                    ImageNames = imageNames,
                    Fee = 4000,
                    Description = "Hotel in Mvdeo",
                    Telephone = "+598",
                    ContactInformation = "Owner",
                    TouristSpot = new TouristSpotImport
                    {
                        Name = "Beach",
                        Description = "asd",
                        Image = "imagen",
                        RegionId = 1,
                        CategoryIds = new List<int> { 1,2 }
                    }
                },

            };

            accomodationMock = new Mock<IRepository<Accomodation>>(MockBehavior.Loose);
            regionMock = new Mock<IRepository<Region>>(MockBehavior.Loose);
            categoryMock = new Mock<IRepository<Category>>(MockBehavior.Loose);
            joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            touristSpotHandlerMock = new Mock<ITouristSpotHandler>(MockBehavior.Strict);
            handler = new AccomodationHandler(accomodationMock.Object, touristSpotHandlerMock.Object);
        }


        [TestMethod]
        public void AddAccomodationWithTouristSpot()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(accomodation.TouristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            var res = handler.Add(accomodation, touristSpot.Id, imageNames);

            accomodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(accomodation, res);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The tourist spot does not exists")]
        public void AddAccomodationWithoutTouristSpot()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns((TouristSpot)null);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation needs a non empty name")]
        public void AddAccomodationWithoutName()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Name = "";
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation needs a non empty name")]
        public void AddAccomodationWithoutName2()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Name = "   ";
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation needs a non empty address")]
        public void AddAccomodationWithoutDirection()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Address = "";
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation needs a non empty address")]
        public void AddAccomodationWithoutDirection2()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Address = "   ";
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation stars needs to be between 1 and 5")]
        public void AddAccomodationWithNegativeStars()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Stars = -1;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation stars needs to be between 1 and 5")]
        public void AddAccomodationWithMoreThan5Stars()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Stars = 5.1;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation stars needs to be between 1 and 5")]
        public void AddAccomodationWith0Stars()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Stars = 0;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation fee needs to be more than 0")]
        public void AddAccomodationWith0Fee()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Fee = 0;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation fee needs to be more than 0")]
        public void AddAccomodationWithNegativeFee()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Fee = -10;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation needs at least one image")]
        public void AddAccomodationWithoutImages()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Images = new List<Image>();
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation needs at least one image")]
        public void AddAccomodationWithNullImages()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            accomodation.Images = null;
            var res = handler.Add(accomodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The Accomodation needs at least one image")]
        public void AddAccomodationWithNullImagesNames()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accomodationMock.Setup(x => x.Add(accomodation)).Returns(accomodation);

            var res = handler.Add(accomodation, touristSpot.Id, null);
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

        [ExpectedException(typeof(NotFoundException))]
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
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);

            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);

            var res = handler.SearchByTouristSpot(touristSpot.Id);

            touristSpotHandlerMock.VerifyAll();
            accomodationMock.VerifyAll();
            Assert.AreEqual(new List<Accomodation> { accomodation }[0], res[0]);
        }

        [TestMethod]
        public void SearchAllAccomodation()
        {
            accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accomodation> { accomodation });
            touristSpotMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);

            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);

            var res = handler.SearchByTouristSpot(touristSpot.Id,false);

            touristSpotMock.VerifyAll();
            accomodationMock.VerifyAll();
            Assert.AreEqual(new List<Accomodation> { accomodation }[0], res[0]);
        }

        [TestMethod]
        public void SearchNonAvailableAccomodation()
        {
            accomodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accomodation> { });

            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);


            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);

            var res = handler.SearchByTouristSpot(touristSpot.Id);

            touristSpotHandlerMock.VerifyAll();
            accomodationMock.VerifyAll();
            Assert.AreEqual(0, res.Count);
        }

        [ExpectedException(typeof(BadRequestException))]
        [TestMethod]
        public void SearchNonExistingSpot()
        {
            touristSpotHandlerMock.Setup(x => x.Get(0)).Returns((TouristSpot)null);

            var res = handler.SearchByTouristSpot(0);

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
        [ExpectedException(typeof(NotFoundException))]
        public void ChangeAccomodationAvaliabilityBadAccomodation()
        {
            accomodationMock.Setup(x => x.Update(accomodation)).Returns(true);
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns((Accomodation)null);

            var res = handler.ChangeAvailability(accomodation.Id, false);

            accomodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void GetAccomodationFalse()
        {
            accomodationMock.Setup(x => x.Get(accomodation.Id)).Returns((Accomodation)null);

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


        [TestMethod]
        public void ImportAccomodations()
        {
            var accomodation = import.ElementAt(0);
            var spot = accomodation.TouristSpot.ToEntity();

            touristSpotHandlerMock.Setup(x => x.Get(accomodation.TouristSpot.Name)).Returns(spot);
            touristSpotHandlerMock.Setup(x => x.Get(spot.Id)).Returns(spot);
            accomodationMock.Setup(x => x.Add(It.IsAny<Accomodation>())).Returns(accomodation.ToEntity());

            var res = handler.Add(import);

            accomodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void ImportAccomodationsNewSpot()
        {
            var accomodation = import.ElementAt(0);
            var spot = accomodation.TouristSpot.ToEntity();

            touristSpotHandlerMock.Setup(x => x.Get(accomodation.TouristSpot.Name)).Returns((TouristSpot)null);
            touristSpotHandlerMock.Setup(x => x.Add(It.IsAny<TouristSpot>(), accomodation.TouristSpot.RegionId,accomodation.TouristSpot.CategoryIds, accomodation.TouristSpot.Image)).Returns(spot);
            touristSpotHandlerMock.Setup(x => x.Get(spot.Id)).Returns(spot);
            accomodationMock.Setup(x => x.Add(It.IsAny<Accomodation>())).Returns(accomodation.ToEntity());

            var res = handler.Add(import);

            accomodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void ImportAccomodationsWrongDataAccomodation()
        {
            var accomodation = import.ElementAt(0);
            var spot = accomodation.TouristSpot.ToEntity();

            touristSpotHandlerMock.Setup(x => x.Get(accomodation.TouristSpot.Name)).Returns((TouristSpot)null);
            touristSpotHandlerMock.Setup(x => x.Add(It.IsAny<TouristSpot>(), accomodation.TouristSpot.RegionId, accomodation.TouristSpot.CategoryIds, accomodation.TouristSpot.Image)).Returns(spot);
            touristSpotHandlerMock.Setup(x => x.Get(spot.Id)).Returns(spot);
            accomodationMock.Setup(x => x.Add(It.IsAny<Accomodation>())).Throws(new BadRequestException());

            var res = handler.Add(import);

            accomodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void ImportAccomodationsWrongDataSpot()
        {
            var accomodation = import.ElementAt(0);
            var spot = accomodation.TouristSpot.ToEntity();

            touristSpotHandlerMock.Setup(x => x.Get(accomodation.TouristSpot.Name)).Returns((TouristSpot)null);
            touristSpotHandlerMock.Setup(x => x.Add(It.IsAny<TouristSpot>(), accomodation.TouristSpot.RegionId, accomodation.TouristSpot.CategoryIds, accomodation.TouristSpot.Image)).Returns((TouristSpot)null);

            var res = handler.Add(import);

            accomodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(false, res);
        }
    }
}
