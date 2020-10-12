using System;

namespace Domain
{

 

    public class Region
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public override bool Equals(object obj)
        {
            return obj is Region region &&
                   Name == region.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }
    }
}