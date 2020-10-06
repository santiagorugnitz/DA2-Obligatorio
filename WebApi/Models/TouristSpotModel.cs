using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class TouristSpotModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public  string Image { get; set; }

        public  int RegionId { get; set; }

        public ICollection<int> CategoryIds { get; set; }

    }
}
