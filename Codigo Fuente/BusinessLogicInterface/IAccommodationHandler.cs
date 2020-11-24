using DataImport;
using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface IaccommodationHandler
    {
        accommodation Add(accommodation accommodation, int touristSpotId, List<string> imageNames);
        bool Add(List<accommodationImport> accommodations);
        bool ChangeAvailability(int Id, bool availability);
        bool Delete(int id);
        bool Exists(accommodation accommodation);
        List<accommodation> SearchByTouristSpot(int touristSpotId, bool onlyAvailable = true);
        accommodation Get(int accommodationId);
        double CalculateTotal(int id, Stay stay);
    }
}