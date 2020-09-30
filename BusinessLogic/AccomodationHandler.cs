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
        private TouristSpotHandler touristSpotHandler;
        private IRepository<Accomodation> accomodationRepository;

        public AccomodationHandler(IRepository<Accomodation> accomodationRepo, TouristSpotHandler touristSpotHand)
        {
            accomodationRepository = accomodationRepo;
            touristSpotHandler = touristSpotHand;
        }

        public bool Add(Accomodation accomodation)
        {
            if (touristSpotHandler.Exists(accomodation.TouristSpot))
            {
                return accomodationRepository.Add(accomodation);
            }
            else
            {
                throw new NullReferenceException("The tourist spot does not exists");
            }
        }

        public object Delete(Accomodation accomodation)
        {
            return accomodationRepository.Delete(accomodation);
        }
        public bool Exists(Accomodation accomodation)
        {
            return accomodationRepository.Get(accomodation.Id) != null;
        }

        public object ChangeAvailability(Accomodation accomodation, bool availability)
        {
            accomodation.Available = availability;
            return accomodationRepository.Update(accomodation);
        }

        public List<Accomodation> SearchByTouristSpot(TouristSpot touristSpot, DateTime checkIn, DateTime checkOut)
        {
            return accomodationRepository.GetAll(x => ((Accomodation)x).TouristSpot.Equals(touristSpot) &&
            ((Accomodation)x).Available).ToList();
        }
    }
}
