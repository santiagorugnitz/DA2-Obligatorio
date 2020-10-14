using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class RegionHandler : IRegionHandler
    {
        private IRepository<Region> repository;

        public RegionHandler(IRepository<Region> regionRepository)
        {
            repository = regionRepository;
        }

        public List<Region> GetAll()
        {
            return repository.GetAll().ToList();
        }
    }
}
