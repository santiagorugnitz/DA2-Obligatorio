using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface IAccomodationHandler
    {
        Accomodation Add(Accomodation accomodation, int touristSpotId, List<string> imageNames);
        bool ChangeAvailability(int Id, bool availability);
        bool Delete(int id);
        bool Exists(Accomodation accomodation);
        List<Accomodation> SearchByTouristSpot(int touristSpotId, bool onlyAvailable = true);
        Accomodation Get(int accomodationId);
    }
}