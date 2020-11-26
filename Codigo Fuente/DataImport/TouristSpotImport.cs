using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataImport
{
    public class TouristSpotImport
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int RegionId { get; set; }

        public int[] Categories { get; set; }

        public string Image { get; set; }

        public TouristSpot ToEntity()
        {
            return new TouristSpot
            {
                Name = this.Name,
                Description = this.Description,
            };
        }
    }
}
