using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class SearchModel
    {
        public int RegionId { get; set; }

        public List<int> CategoryIds { get; set; }
    }
}
