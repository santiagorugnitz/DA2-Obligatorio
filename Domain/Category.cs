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

        public virtual IEnumerable<TouristSpotCategory> TouristSpotCategories { get; set; }
    }
}