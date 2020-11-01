using System;
using System.Collections.Generic;
using System.Text;

namespace DataImport
{
    public class AccomodationImport
    {
        public string Name { get; set; }

        public double Stars { get; set; }

        public string Address { get; set; }

        public List<string> ImageNames { get; set; }

        public double Fee { get; set; }

        public string Description { get; set; }

        public bool Available { get; set; }

        public string Telephone { get; set; }

        public string ContactInformation { get; set; }

        public TouristSpotImport TouristSpot { get; set; }
    }
}
