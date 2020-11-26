using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataImport
{
    public class AccommodationImport
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Stars { get; set; }

        public string Address { get; set; }
        
        public bool Available { get; set; }

        public List<string> Images { get; set; }

        public double Fee { get; set; }

        public string Telephone { get; set; }

        public string ContactInformation { get; set; }

        public TouristSpotImport TouristSpot { get; set; }

        public Accommodation ToEntity() => new Accommodation()
        {
            Name = this.Name,
            Stars = this.Stars,
            Address = this.Address,
            Fee = this.Fee,
            Description = this.Description,
            Telephone = this.Telephone,
            ContactInformation = this.ContactInformation,
            Available = this.Available
        };
    }
}
