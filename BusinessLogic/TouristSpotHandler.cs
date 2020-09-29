using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

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
            return repository.Get(touristSpot.Id) != null;
        }

        public List<TouristSpot> SearchByRegion(Region region)
        {
            return repository.GetAll(x => ((TouristSpot)x).Region.Equals(region)).ToList();
        }

        public List<TouristSpot> SearchByCategory(Category category)
        {
            return repository.GetAll(x => ((TouristSpot)x).Categories.Contains(category)).ToList();
        }

        public List<TouristSpot> SearchByRegionAndCategory(Category category, Region region)
        {
            return repository.GetAll(x => ((TouristSpot)x).Categories.Contains(category) &&
            ((TouristSpot)x).Categories.Contains(category)).ToList();
        }
    }
}
