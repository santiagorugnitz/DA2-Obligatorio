﻿using BusinessLogicInterface;
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
            if (spotsRepository.GetAll(x => ((TouristSpot) x).Name == spot.Name).
                ToList().Count() > 0)
            {
                throw new ArgumentNullException("The name already exists");
            }

            var gotRegion = regionRepository.Get(regionId);
            if (gotRegion == null)
            {
                throw new ArgumentNullException("The region does not exists");
            }

            spot.Region = gotRegion;

            Image image = new Image { Name = imageName };
            imageRepository.Add(image);
            spot.Image = image;

            List<TouristSpotCategory> gotCategories = new List<TouristSpotCategory>();

            foreach (var item in categoryIds)
            {
                var gotCategory = categoryRepository.Get(item);
                
                if (gotCategory == null)
                {
                    throw new ArgumentNullException("A category does not exists");
                }

                gotCategories.Add(new TouristSpotCategory { CategoryId = item, Category = gotCategory,
                TouristSpotId = spot.Id, TouristSpot = spot});

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
