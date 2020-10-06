using Domain;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface ITouristSpotHandler
    {
        bool Add(TouristSpot spot);
        object Delete(TouristSpot spot);
        TouristSpot Get(int Id);
        List<TouristSpot> Search(List<Category> categories, Region region);
    }
}