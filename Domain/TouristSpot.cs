using Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class TouristSpot
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The spot needs a non empty Name");
                }
                else
                {
                    name = value.Trim();
                }
            }

            get { return name; }
        }

        private string description;
        public string Description
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Trim().Length > 2000)
                {
                    throw new BadRequestException("The spot needs a correct description (less than 2000 characters) and non empty");
                }
                else
                {
                    description = value.Trim();
                }
            }

            get { return description; }
        }

        public virtual Image Image { get; set; }

        public virtual Region Region { get; set; }

        private ICollection<TouristSpotCategory> touristSpotCategories;
        public virtual ICollection<TouristSpotCategory> TouristSpotCategories
        {
            get { return touristSpotCategories; }
            set
            {
                if (value == null || value.Count == 0)
                {
                    throw new BadRequestException("The spot needs at least one category");
                }
                else
                {
                    touristSpotCategories = value;
                }
            }
        }
        public override bool Equals(object obj)
        {
            return obj is TouristSpot spot &&
                   spot.Id == Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, Image, Region);
        }
    }
}
