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

            List<TouristSpotCategory> gotCategories = GenerateCategoriesList(spot, categoryIds);

            var addingResult = spotsRepository.Add(spot);

            foreach (var category in gotCategories)
            {
                category.TouristSpotId = spot.Id;
                joinedRepository.Add(category);
            }

            return addingResult;
        }

        private List<TouristSpotCategory> GenerateCategoriesList(TouristSpot spot, List<int> categoryIds)
        {
            List<TouristSpotCategory> gotCategories = new List<TouristSpotCategory>();

            foreach (var category in categoryIds)
            {
                var gotCategory = categoryRepository.Get(category);

                if (gotCategory == null)
                {
                    throw new BadRequestException("A category does not exist");
                }

                gotCategories.Add(new TouristSpotCategory
                {
                    CategoryId = category,
                    Category = gotCategory,
                    TouristSpotId = spot.Id,
                    TouristSpot = spot
                });
            }

            return gotCategories;
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

            List<TouristSpot> spotsList;
            spotsList = GetListByRegion(region);

            List<TouristSpot> returningSpotList = new List<TouristSpot>();

            foreach (var spot in spotsList)
            {
                var hasAll = HasAllCategories(categories, spot);
                if (hasAll) returningSpotList.Add(spot);
            }

            return returningSpotList;
        }

        private bool HasAllCategories(List<int> categories, TouristSpot spot)
        {
            var hasAll = true;
            foreach (var category in categories)
            {
                if (categoryRepository.Get(category) == null)
                {
                    throw new BadRequestException("A category does not exist");
                }
                hasAll = spot.TouristSpotCategories.Any(x => x.CategoryId == category);
                if (!hasAll) break;
            }

            return hasAll;
        }

        private List<TouristSpot> GetListByRegion(int? region)
        {
            List<TouristSpot> spotsList;

            if (region == null)
            {
                spotsList = spotsRepository.GetAll().ToList();
            }
            else
            {
                if (region.HasValue && regionRepository.Get(region.Value) == null)
                {
                    throw new BadRequestException("The region does not exist");
                }
                spotsList = spotsRepository.GetAll(x => ((TouristSpot)x).Region.Id == region).ToList();
            }

            return spotsList;
        }
    }
}
