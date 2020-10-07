﻿using DataAccessInterface;
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

        public bool Add(Accomodation accomodation, int touristSpotId, List<string> imageNames)
        {
            foreach (var item in imageNames)
            {
                Image image = new Image { Name = item };
                imageRepository.Add(image);
            }

            if (touristSpotHandler.Get(touristSpotId)==null)
            {
                throw new ArgumentNullException("The tourist spot does not exists");
            }

            return accomodationRepository.Add(accomodation);
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

        public bool Exists(int accomodationId)
        {
            return accomodationRepository.Get(accomodationId) != null;
        }
    }
}
