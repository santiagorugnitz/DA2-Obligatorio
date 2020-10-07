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
        private IRepository<Image> imageRepository;
        private IRepository<Category> categoryRepository;
        private IRepository<Region> regionRepository;
        private IRepository<TouristSpotCategory> joinedRepository;

        public TouristSpotHandler(IRepository<TouristSpot> repo, IRepository<Image> imageRepo,
            IRepository<Category> categoryRepo, IRepository<Region> regionRepo
            , IRepository<TouristSpotCategory> joinedRepo)
        {
            spotsRepository = repo;
            imageRepository = imageRepo;
            categoryRepository = categoryRepo;
            regionRepository = regionRepo;
            joinedRepository = joinedRepo;
        }

        public bool Add(TouristSpot spot, int regionId, List<int> categoryIds, string imageName)
        {
            if (regionRepository.Get(regionId) == null)
            {
                regionRepository.Add(spot.Region);
            }

            if (imageRepository.GetAll(x => ((Image) x).Name == imageName).Count() == 0)
            {
                imageRepository.Add(spot.Image);
            }

            foreach (var item in categoryIds)
            {
                if (categoryRepository.Get(item) == null)
                {
                    categoryRepository.Add(spot.TouristSpotCategories.
                        Where(x => x.CategoryId == item).ToList()[0].Category);
                }
            }

            return spotsRepository.Add(spot);
        }

        public TouristSpot Get(int id)
        {
            return spotsRepository.Get(id);
        }

        public List<TouristSpot> Search(List<int> categories = null, int? region = null)
        {
            var joinedEntry = new List<TouristSpotCategory>();
            
            if(categories==null) return spotsRepository.GetAll(x => ((TouristSpot)x).Region.Id==region).ToList();

            foreach (var cat in categories)
            {
                joinedEntry.AddRange(joinedRepository.GetAll(x => ((TouristSpotCategory)x).CategoryId == cat));
            }
            var spots = new List<TouristSpot>();
            foreach (var entry in joinedEntry)
            {
                if (!spots.Contains(entry.TouristSpot)) spots.Add(entry.TouristSpot);
            }
            if(region!=null) return spots.FindAll(x => x.Region.Id == region);
            return spots;
        }
    }
}
