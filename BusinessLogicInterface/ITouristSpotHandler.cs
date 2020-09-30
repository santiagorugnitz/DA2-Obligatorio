using Domain;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface ITouristSpotHandler
    {
        bool Add(TouristSpot spot);
        object Delete(TouristSpot spot);
        bool Exists(TouristSpot touristSpot);
        List<TouristSpot> SearchByCategory(Category category);
        List<TouristSpot> SearchByRegion(Region region);
        List<TouristSpot> SearchByRegionAndCategory(Category category, Region region);
    }
}