using System;
using System.Collections.Generic;

namespace Domain
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        public virtual ICollection<TouristSpotCategory> TouristSpotCategories { get; set; }
    }
}