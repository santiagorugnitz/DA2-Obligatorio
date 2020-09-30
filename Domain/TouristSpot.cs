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

        public virtual Region Region { get; set; }

        public virtual ICollection<TouristSpotCategory> TouristSpotCategories { get; set; }
        
    }
}
