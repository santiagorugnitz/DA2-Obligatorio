using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogicInterface;

namespace BusinessLogic
{
    public class AccomodationHandler : IAccomodationHandler
    {
        private ITouristSpotHandler touristSpotHandler;
        private IRepository<Accomodation> accomodationRepository;
        private IRepository<Image> imageRepository;

        public AccomodationHandler(IRepository<Accomodation> accomodationRepo,
            IRepository<Image> imageRepo, ITouristSpotHandler touristSpotHand)
        {
            accomodationRepository = accomodationRepo;
            imageRepository = imageRepo;
            touristSpotHandler = touristSpotHand;
        }

        public Accomodation Add(Accomodation accomodation, int touristSpotId, List<string> imageNames)
        {
            List<Image> accomodationImages = new List<Image>();

            foreach (var item in imageNames)
            {
                Image image = new Image { Name = item };
                imageRepository.Add(image);
                accomodationImages.Add(image);
            }

            var gotTouristSpot = touristSpotHandler.Get(touristSpotId);

            if (gotTouristSpot == null)
            {
                throw new ArgumentNullException("The tourist spot does not exist");
            }

            accomodation.Images = accomodationImages;
            accomodation.TouristSpot = gotTouristSpot;

            return accomodationRepository.Add(accomodation);
        }

        public bool Delete(int id)
        {
            var accomodation = Get(id);
            if (accomodation == null) throw new NullReferenceException("There is no accomodation with that id");

            return accomodationRepository.Delete(accomodation);
        }
        public bool Exists(Accomodation accomodation)
        {
            return accomodationRepository.Get(accomodation.Id) != null;
        }

        public bool ChangeAvailability(int id, bool availability)
        {
            var accomodation = Get(id);
            if (accomodation == null) throw new NullReferenceException("There is no accomodation with that id");

            accomodation.Available = availability;
            return accomodationRepository.Update(accomodation);
        }

        public List<Accomodation> SearchByTouristSpot(int spotId)
        {
            return accomodationRepository.GetAll(x => ((Accomodation)x).TouristSpot.Id==spotId &&
            ((Accomodation)x).Available).ToList();
        }

        public Accomodation Get(int accomodationId)
        {
            return accomodationRepository.Get(accomodationId);
        }
    }
}
