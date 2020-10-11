using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BusinessLogic
{
    public class TouristSpotHandler : ITouristSpotHandler
    {

        private IRepository<TouristSpot> spotsRepository;
        private IRepository<Category> categoryRepository;
        private IRepository<Region> regionRepository;
        private IRepository<TouristSpotCategory> joinedRepository;

        public TouristSpotHandler(IRepository<TouristSpot> repo, 
            IRepository<Category> categoryRepo, IRepository<Region> regionRepo
            , IRepository<TouristSpotCategory> joinedRepo)
        {
            spotsRepository = repo;
            categoryRepository = categoryRepo;
            regionRepository = regionRepo;
            joinedRepository = joinedRepo;
        }

        public TouristSpot Add(TouristSpot spot, int regionId, List<int> categoryIds, string imageName)
        {
            if (categoryIds.Count == 0) throw new BadRequestException("The spot needs at least one category");

            if (spotsRepository.GetAll(x => ((TouristSpot)x).Name == spot.Name).
                ToList().Count() > 0)
            {
                throw new BadRequestException("The name already exists");
            }

            var gotRegion = regionRepository.Get(regionId);
            if (gotRegion == null)
            {
                throw new BadRequestException("The region does not exist");
            }

            spot.Region = gotRegion;

            spot.Image = new Image { Name = imageName };

            List<TouristSpotCategory> gotCategories = new List<TouristSpotCategory>();

            foreach (var item in categoryIds)
            {
                var gotCategory = categoryRepository.Get(item);

                if (gotCategory == null)
                {
                    throw new BadRequestException("A category does not exist");
                }

                gotCategories.Add(new TouristSpotCategory
                {
                    CategoryId = item,
                    Category = gotCategory,
                    TouristSpotId = spot.Id,
                    TouristSpot = spot
                });

            }

            var result = spotsRepository.Add(spot);

            foreach (var item in gotCategories)
            {
                item.TouristSpotId = spot.Id;
                joinedRepository.Add(item);
            }

            return result;
        }

        public TouristSpot Get(int id)
        {
            return spotsRepository.Get(id);
        }

        public List<TouristSpot> Search(List<int> categories = null, int? region = null)
        {
            if (categories == null)
            {
                if (region.HasValue && regionRepository.Get(region.Value) == null)
                {
                    throw new BadRequestException("The region does not exist");
                }
                return spotsRepository.GetAll(x => ((TouristSpot)x).Region.Id == region).ToList();
            }

            List<TouristSpot> list;
            if (region == null)
            {
                list = spotsRepository.GetAll().ToList();
            }
            else
            {
                if (region.HasValue && regionRepository.Get(region.Value) == null)
                {
                    throw new BadRequestException("The region does not exist");
                }
                list = spotsRepository.GetAll(x => ((TouristSpot)x).Region.Id == region).ToList();
            }

            List<TouristSpot> ret = new List<TouristSpot>();

            foreach (var spot in list)
            {
                var hasAll = true;
                foreach (var cat in categories)
                {
                    if (categoryRepository.Get(cat) == null)
                    {
                        throw new BadRequestException("A category does not exist");
                    }
                    hasAll = spot.TouristSpotCategories.Any(x => x.CategoryId == cat);
                    if (!hasAll) break;
                }
                if (hasAll) ret.Add(spot);
            }
            return ret;
        }
    }
}
