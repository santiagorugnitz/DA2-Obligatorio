﻿using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class AccomodationHandler
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
            return accomodationRepository.Exists(accomodation);
        }
    }
}
