using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class accommodationModel
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

        public int TouristSpotId { get; set; }

        public accommodation ToEntity() => new accommodation()
        {
            Name = this.Name,
            Stars = this.Stars,
            Address = this.Address,
            Fee = this.Fee,
            Description = this.Description,
            Telephone = this.Telephone,
            ContactInformation = this.ContactInformation,
            Available=this.Available
        };
    }
}
