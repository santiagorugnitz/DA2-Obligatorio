using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class TouristSpotHandler
    {

        private IRepository<TouristSpot> spotsRepository;
        private IRepository<TouristSpotCategory> joinedRepository;

        public TouristSpotHandler(IRepository<TouristSpot> repo, IRepository<TouristSpotCategory> joinedRepo)
        {
            spotsRepository = repo;
            joinedRepository = joinedRepo;
        }

        public bool Add(TouristSpot spot)
        {
            return spotsRepository.Add(spot);
        }

        public object Delete(TouristSpot spot)
        {
            return spotsRepository.Delete(spot);
        }

        public bool Exists(TouristSpot touristSpot)
        {
            return spotsRepository.Get(touristSpot.Id) != null;
        }

        public List<TouristSpot> SearchByRegion(Region region)
        {
            return spotsRepository.GetAll(x => ((TouristSpot)x).Region.Equals(region)).ToList();
        }

        public List<TouristSpot> SearchByCategory(Category category)
        {
            var joinedEntry = joinedRepository.Get(category.Id);

            return spotsRepository.GetAll(x => ((TouristSpot)x).Id == joinedEntry.TouristSpotId).ToList();
        }

        public List<TouristSpot> SearchByRegionAndCategory(Category category, Region region)
        {
            var joinedEntry = joinedRepository.Get(category.Id);

            return spotsRepository.GetAll(x => ((TouristSpot)x).Id == joinedEntry.TouristSpotId &&
            ((TouristSpot)x).Region.Id == region.Id).ToList();
        }
    }
}
