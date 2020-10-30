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

            if (imageNames == null)
            {
                throw new BadRequestException("The accommodation needs at least one image");
            }

            foreach (var imageName in imageNames)
            {
                Image image = new Image { Name = imageName };
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
            if (accomodation == null) throw new NotFoundException("The accomodation does not exist");

            return accomodationRepository.Delete(accomodation);
        }
        public bool Exists(Accomodation accomodation)
        {
            return accomodationRepository.Get(accomodation.Id) != null;
        }

        public bool ChangeAvailability(int id, bool availability)
        {
            var accomodation = Get(id);
            if (accomodation == null) throw new NotFoundException("The accomodation does not exist");

            accomodation.Available = availability;
            return accomodationRepository.Update(accomodation);
        }

        public List<Accomodation> SearchByTouristSpot(int spotId)
        {
            if (touristSpotHandler.Get(spotId) == null)
            {
                throw new BadRequestException("The spot does not exist");
            }

            return accomodationRepository.GetAll(x => ((Accomodation)x).TouristSpot.Id == spotId &&
            ((Accomodation)x).Available).ToList();


        }

        public Accomodation Get(int accomodationId)
        {
            return accomodationRepository.Get(accomodationId);
        }

        public bool Add(List<Accomodation> accomodations)
        {
            throw new NotImplementedException();
        }
    }
}
