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
                    throw new ArgumentNullException("The spot needs a non emty Name");
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
        private string imageUrl;
        public string ImageUrl
        {
            set
            {
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The spot needs a non empty picture URL");
                }
                else
                {
                    imageUrl = value.Trim();
                }
            }

            get { return imageUrl; }
        }

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

    }
}
