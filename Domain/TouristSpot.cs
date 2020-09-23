using System;
using System.Collections.Generic;

namespace Domain
{
    public class TouristSpot
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public Region Region { get; set; }

        public List<Category> Categories { get; set; }
    }
}
