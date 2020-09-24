﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Accomodation
    {
        public string Name { get; set; }

        public double Stars { get; set; }

        public string Address { get; set; }

        public List<string> ImageUrlList { get; set; }

        public List<Category> Categories { get; set; }
        
        public double Fee { get; set; }

        public string Description { get; set; }

        public bool Available { get; set; }

        public string Telephone { get; set; }

        public string ContactInformation { get; set; }

        public TouristSpot TouristSpot { get; set; }

    }
}
