using Domain;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface ITouristSpotHandler
    {
        bool Add(TouristSpot spot, int regionId, List<int> categoryIds, string imageName);
        TouristSpot Get(int Id);
        List<TouristSpot> Search(List<int> categories, int? region);
    }
}