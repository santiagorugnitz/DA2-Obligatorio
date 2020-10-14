using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TouristSpotCategory
    {
        public int TouristSpotId { get; set; }

        public virtual TouristSpot TouristSpot { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}
