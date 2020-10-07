using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface IAccomodationHandler
    {
        bool Add(Accomodation accomodation, int touristSpotId, List<string> imageNames);
        bool ChangeAvailability(int Id, bool availability);
        bool Delete(Accomodation accomodation);
        bool Exists(Accomodation accomodation);
        List<Accomodation> SearchByTouristSpot(TouristSpot touristSpot, DateTime checkIn, DateTime checkOut);
        Accomodation Get(int accomodationId);
    }
}