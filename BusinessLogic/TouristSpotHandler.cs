using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
    public class TouristSpotHandler : ITouristSpotHandler
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

        public TouristSpot Get(int id)
        {
            return spotsRepository.Get(id);
        }

        public List<TouristSpot> Search(List<Category> categories = null, Region region = null)
        {
            var joinedEntry = new List<TouristSpotCategory>();
            
            if(categories==null) return spotsRepository.GetAll(x => ((TouristSpot)x).Region.Equals(region)).ToList();

            foreach (var cat in categories)
            {
                joinedEntry.AddRange(joinedRepository.GetAll(x => ((TouristSpotCategory)x).CategoryId == cat.Id));
            }
            var spots = new List<TouristSpot>();
            foreach (var entry in joinedEntry)
            {
                if (!spots.Contains(entry.TouristSpot)) spots.Add(entry.TouristSpot);
            }
            if(region!=null) return spots.FindAll(x => x.Region.Id == region.Id);
            return spots;
        }
    }
}
