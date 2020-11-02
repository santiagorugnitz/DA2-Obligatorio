using Domain;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface ITouristSpotHandler
    {
        TouristSpot Add(TouristSpot spot, int regionId, List<int> categoryIds, string imageName);
        TouristSpot Get(int Id);
        TouristSpot Get(string name);
        List<TouristSpot> Search(List<int> categories, int? region);
    }
}