using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;

namespace BusinessLogic
{
    public class TouristSpotHandler
    {

        private IRepository<TouristSpot> repository;

        public TouristSpotHandler(IRepository<TouristSpot> repo)
        {
            repository = repo;
        }

        public bool Add(TouristSpot spot)
        {
            return repository.Add(spot);
        }

        public object Delete(TouristSpot spot)
        {
            return repository.Delete(spot);
        }

        public bool Exists(TouristSpot touristSpot)
        {
            return repository.Exists(touristSpot);
        }

        public List<TouristSpot> SearchByRegion(Region region)
        {
            return repository.Filter(x => ((TouristSpot)x).Region.Equals(region));
        }

        public List<TouristSpot> SearchByCategory(Category category)
        {
            return repository.Filter(x => ((TouristSpot)x).Categories.Contains(category));
        }

        public List<TouristSpot> SearchByRegionAndCategory(Category category, Region region)
        {
            return repository.Filter(x => ((TouristSpot)x).Categories.Contains(category) &&
            ((TouristSpot)x).Categories.Contains(category));
        }
    }
}
