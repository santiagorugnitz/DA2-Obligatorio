using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLogicInterface;
using Exceptions;

namespace BusinessLogic
{
    public class AccomodationHandler : IAccomodationHandler
    {
        private ITouristSpotHandler touristSpotHandler;
        private IRepository<Accomodation> accomodationRepository;
        
        public AccomodationHandler(IRepository<Accomodation> accomodationRepo,
            ITouristSpotHandler touristSpotHand)
        {
            accomodationRepository = accomodationRepo;
            touristSpotHandler = touristSpotHand;
        }

        public Accomodation Add(Accomodation accomodation, int touristSpotId, List<string> imageNames)
        {
            List<Image> accomodationImages = new List<Image>();

            foreach (var item in imageNames)
            {
                Image image = new Image { Name = item };
                accomodationImages.Add(image);
            }

            var gotTouristSpot = touristSpotHandler.Get(touristSpotId);

            if (gotTouristSpot == null)
            {
                throw new BadRequestException("The tourist spot does not exist");
            }

            accomodation.Images = accomodationImages;
            accomodation.TouristSpot = gotTouristSpot;

            return accomodationRepository.Add(accomodation);
        }

        public bool Delete(int id)
        {
            var accomodation = Get(id);
            if (accomodation == null) throw new NotFoundException("There is no accomodation with that id");

            return accomodationRepository.Delete(accomodation);
        }
        public bool Exists(Accomodation accomodation)
        {
            return accomodationRepository.Get(accomodation.Id) != null;
        }

        public bool ChangeAvailability(int id, bool availability)
        {
            var accomodation = Get(id);
            if (accomodation == null) throw new NotFoundException("There is no accomodation with that id");

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
