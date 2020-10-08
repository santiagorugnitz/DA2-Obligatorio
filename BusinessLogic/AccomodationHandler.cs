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

            if (gotTouristSpot==null)
            {
                throw new ArgumentNullException("The tourist spot does not exists");
            }

            accomodation.Images = accomodationImages;
            accomodation.TouristSpot = gotTouristSpot;

            return accomodationRepository.Add(accomodation);
        }

        public bool Delete(int id)
        {
            return accomodationRepository.Delete(Get(id));
        }
        public bool Exists(Accomodation accomodation)
        {
            return accomodationRepository.Get(accomodation.Id) != null;
        }

        public bool ChangeAvailability(int id, bool availability)
        {
            var accomodation = accomodationRepository.Get(id);
            if (accomodation == null)
            {
                throw new ArgumentNullException("The accomodation does not exists");
            }
            else
            {
                accomodation.Available = availability;
                return accomodationRepository.Update(accomodation);
            }
        }

        public List<Accomodation> SearchByTouristSpot(TouristSpot touristSpot, DateTime checkIn, DateTime checkOut)
        {
            return accomodationRepository.GetAll(x => ((Accomodation)x).TouristSpot.Equals(touristSpot) &&
            ((Accomodation)x).Available).ToList();
        }

        public Accomodation Get(int accomodationId)
        {
            return accomodationRepository.Get(accomodationId);
        }
    }
}
