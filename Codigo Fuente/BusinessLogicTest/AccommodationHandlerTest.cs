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
    public class AccommodationHandlerTest
    {
        private Accommodation accommodation;
        private TouristSpot touristSpot;
        private Mock<IRepository<Accommodation>> accommodationMock;
        private Mock<IRepository<Region>> regionMock;
        private Mock<IRepository<Category>> categoryMock;
        private Mock<IRepository<TouristSpotCategory>> joinedMock;
        private Mock<ITouristSpotHandler> touristSpotHandlerMock;
        private AccommodationHandler handler;
        private List<string> imageNames;
        private List<AccommodationImport> import;

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

            accommodation = new Accommodation()
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

            import = new List<AccommodationImport>() {
                new AccommodationImport
                {
                    Name = "Hotel",
                    Stars = 4.0,
                    Address = "Cuareim",
                    Available = true,
                    Images = imageNames,
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
                        Categories = new int[] { 1,2 }
                    }
                },

            };

            accommodationMock = new Mock<IRepository<Accommodation>>(MockBehavior.Loose);
            regionMock = new Mock<IRepository<Region>>(MockBehavior.Loose);
            categoryMock = new Mock<IRepository<Category>>(MockBehavior.Loose);
            joinedMock = new Mock<IRepository<TouristSpotCategory>>(MockBehavior.Strict);
            touristSpotHandlerMock = new Mock<ITouristSpotHandler>(MockBehavior.Strict);
            handler = new AccommodationHandler(accommodationMock.Object, touristSpotHandlerMock.Object);
        }


        [TestMethod]
        public void AddaccommodationWithTouristSpot()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(accommodation.TouristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            var res = handler.Add(accommodation, touristSpot.Id, imageNames);

            accommodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(accommodation, res);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The tourist spot does not exists")]
        public void AddaccommodationWithoutTouristSpot()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns((TouristSpot)null);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation needs a non empty name")]
        public void AddaccommodationWithoutName()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Name = "";
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation needs a non empty name")]
        public void AddaccommodationWithoutName2()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Name = "   ";
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation needs a non empty address")]
        public void AddaccommodationWithoutDirection()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Address = "";
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation needs a non empty address")]
        public void AddaccommodationWithoutDirection2()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Address = "   ";
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation stars needs to be between 1 and 5")]
        public void AddaccommodationWithNegativeStars()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Stars = -1;
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation stars needs to be between 1 and 5")]
        public void AddaccommodationWithMoreThan5Stars()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Stars = 5.1;
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation stars needs to be between 1 and 5")]
        public void AddaccommodationWith0Stars()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Stars = 0;
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation fee needs to be more than 0")]
        public void AddaccommodationWith0Fee()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Fee = 0;
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation fee needs to be more than 0")]
        public void AddaccommodationWithNegativeFee()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Fee = -10;
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation needs at least one image")]
        public void AddaccommodationWithoutImages()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Images = new List<Image>();
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation needs at least one image")]
        public void AddaccommodationWithNullImages()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            accommodation.Images = null;
            var res = handler.Add(accommodation, touristSpot.Id, imageNames);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException),
    "The accommodation needs at least one image")]
        public void AddaccommodationWithNullImagesNames()
        {
            touristSpotHandlerMock.Setup(x => x.Get(touristSpot.Id)).Returns(touristSpot);
            accommodationMock.Setup(x => x.Add(accommodation)).Returns(accommodation);

            var res = handler.Add(accommodation, touristSpot.Id, null);
        }

        [TestMethod]
        public void Deleteaccommodation()
        {
            accommodationMock.Setup(x => x.Delete(accommodation)).Returns(true);
            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);
            var res = handler.Delete(accommodation.Id);

            accommodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [ExpectedException(typeof(NotFoundException))]
        [TestMethod]
        public void DeleteaccommodationWrongId()
        {
            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns((Accommodation)null);

            var res = handler.Delete(accommodation.Id);

        }

        [TestMethod]
        public void Existsaccommodation()
        {
            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);

            var res = handler.Exists(accommodation);

            accommodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void SearchAvailableaccommodation()
        {
            accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accommodation> { accommodation });
            touristSpotHandlerMock.Setup(x => x.Get(1)).Returns(touristSpot);

            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);

            var res = handler.SearchByTouristSpot(1);

            touristSpotHandlerMock.VerifyAll();
            accommodationMock.VerifyAll();
            Assert.AreEqual(new List<Accommodation> { accommodation }[0], res[0]);
        }

        [TestMethod]
        public void SearchAllaccommodation()
        {
            accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accommodation> { accommodation });

            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);

            var res = handler.SearchByTouristSpot(0,false);

            touristSpotHandlerMock.VerifyAll();
            accommodationMock.VerifyAll();
            Assert.AreEqual(new List<Accommodation> { accommodation }[0], res[0]);
        }

        [TestMethod]
        public void SearchAllAvailableaccommodation()
        {
            accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accommodation> { accommodation });

            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);

            var res = handler.SearchByTouristSpot(0, true);

            touristSpotHandlerMock.VerifyAll();
            accommodationMock.VerifyAll();
            Assert.AreEqual(new List<Accommodation> { accommodation }[0], res[0]);
        }

        [TestMethod]
        public void SearchNonAvailableaccommodation()
        {
            accommodationMock.Setup(x => x.GetAll(It.IsAny<Func<object, bool>>())).
                Returns(new List<Accommodation> { });

            touristSpotHandlerMock.Setup(x => x.Get(1)).Returns(touristSpot);


            DateTime checkIn, checkOut = new DateTime();
            checkIn = DateTime.Now;
            checkOut.AddDays(10);

            var res = handler.SearchByTouristSpot(1);

            touristSpotHandlerMock.VerifyAll();
            accommodationMock.VerifyAll();
            Assert.AreEqual(0, res.Count);
        }

        [ExpectedException(typeof(BadRequestException))]
        [TestMethod]
        public void SearchNonExistingSpot()
        {
            touristSpotHandlerMock.Setup(x => x.Get(-1)).Returns((TouristSpot)null);

            var res = handler.SearchByTouristSpot(-1);

        }

        [TestMethod]
        public void ChangeaccommodationAvaliabilityOk()
        {
            accommodationMock.Setup(x => x.Update(accommodation)).Returns(true);
            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);

            var res = handler.ChangeAvailability(accommodation.Id, false);

            accommodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void ChangeaccommodationAvaliabilityBadaccommodation()
        {
            accommodationMock.Setup(x => x.Update(accommodation)).Returns(true);
            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns((Accommodation)null);

            var res = handler.ChangeAvailability(accommodation.Id, false);

            accommodationMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void GetaccommodationFalse()
        {
            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns((Accommodation)null);

            var res = handler.Get(accommodation.Id);

            accommodationMock.VerifyAll();
            Assert.AreEqual(null, res);
        }

        [TestMethod]
        public void GetaccommodationTrue()
        {
            accommodationMock.Setup(x => x.Get(accommodation.Id)).Returns(accommodation);

            var res = handler.Get(accommodation.Id);

            accommodationMock.VerifyAll();
            Assert.AreEqual(accommodation, res);
        }


        [TestMethod]
        public void Importaccommodations()
        {
            var accommodation = import.ElementAt(0);
            var spot = accommodation.TouristSpot.ToEntity();

            touristSpotHandlerMock.Setup(x => x.Get(accommodation.TouristSpot.Name)).Returns(spot);
            touristSpotHandlerMock.Setup(x => x.Get(spot.Id)).Returns(spot);
            accommodationMock.Setup(x => x.Add(It.IsAny<Accommodation>())).Returns(accommodation.ToEntity());

            var res = handler.Add(import);

            accommodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void ImportaccommodationsNewSpot()
        {
            var accommodation = import.ElementAt(0);
            var spot = accommodation.TouristSpot.ToEntity();

            touristSpotHandlerMock.Setup(x => x.Get(accommodation.TouristSpot.Name)).Returns((TouristSpot)null);
            touristSpotHandlerMock.Setup(x => x.Add(It.IsAny<TouristSpot>(), accommodation.TouristSpot.RegionId, It.IsAny<List<int>>(), accommodation.TouristSpot.Image)).Returns(spot);
            touristSpotHandlerMock.Setup(x => x.Get(spot.Id)).Returns(spot);
            accommodationMock.Setup(x => x.Add(It.IsAny<Accommodation>())).Returns(accommodation.ToEntity());

            var res = handler.Add(import);

            accommodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void ImportaccommodationsWrongDataaccommodation()
        {
            var accommodation = import.ElementAt(0);
            var spot = accommodation.TouristSpot.ToEntity();

            touristSpotHandlerMock.Setup(x => x.Get(accommodation.TouristSpot.Name)).Returns((TouristSpot)null);
            touristSpotHandlerMock.Setup(x => x.Add(It.IsAny<TouristSpot>(), accommodation.TouristSpot.RegionId, It.IsAny<List<int>>(), accommodation.TouristSpot.Image)).Returns(spot);
            touristSpotHandlerMock.Setup(x => x.Get(spot.Id)).Returns(spot);
            accommodationMock.Setup(x => x.Add(It.IsAny<Accommodation>())).Throws(new BadRequestException());

            var res = handler.Add(import);

            accommodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void ImportaccommodationsWrongDataSpot()
        {
            var accommodation = import.ElementAt(0);
            var spot = accommodation.TouristSpot.ToEntity();

            touristSpotHandlerMock.Setup(x => x.Get(accommodation.TouristSpot.Name)).Returns((TouristSpot)null);
            touristSpotHandlerMock.Setup(x => x.Add(It.IsAny<TouristSpot>(), accommodation.TouristSpot.RegionId, It.IsAny<List<int>>(), accommodation.TouristSpot.Image)).Returns((TouristSpot)null);

            var res = handler.Add(import);

            accommodationMock.VerifyAll();
            touristSpotHandlerMock.VerifyAll();
            Assert.AreEqual(false, res);
        }

        [TestMethod]
        [ExpectedException(typeof(NotFoundException))]
        public void CalculateWrongId()
        {
            accommodationMock.Setup(x => x.Get(1)).Returns((Accommodation)null);

            var res = handler.CalculateTotal(1,null);

            accommodationMock.VerifyAll();
            Assert.AreEqual(null, res);
        }
    }
}
