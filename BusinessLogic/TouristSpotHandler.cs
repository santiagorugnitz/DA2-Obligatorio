using DataAccessInterface;
using Domain;
using System;

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
    }
}
