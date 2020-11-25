using DataImport;
using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface IAccommodationHandler
    {
        Accommodation Add(Accommodation accommodation, int touristSpotId, List<string> imageNames);
        bool Add(List<AccommodationImport> accommodations);
        bool ChangeAvailability(int Id, bool availability);
        bool Delete(int id);
        bool Exists(Accommodation accommodation);
        List<Accommodation> SearchByTouristSpot(int touristSpotId, bool onlyAvailable = true);
        Accommodation Get(int accommodationId);
        double CalculateTotal(int id, Stay stay);
    }
}