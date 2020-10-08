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
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The spot needs a non empty Name");
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
                if (value.Trim().Length > 2000)
                {
                    throw new ArgumentOutOfRangeException("The spot needs a shorter description (less than 2000 characters)");
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
                if (value.Count == 0)
                {
                    throw new ArgumentNullException("The spot needs at least one category");
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
    }
}
