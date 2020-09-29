using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class TouristSpot
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public Region Region { get; set; }

        public IEnumerable<TouristSpotCategory> TouristSpotCategories { get; set; }
        
        [NotMapped]
        public List<Category> Categories { get; set; }
    }
}
