using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TouristSpotCategory
    {
        public int TouristSpotId { get; set; }

        public TouristSpot TouristSpot { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
