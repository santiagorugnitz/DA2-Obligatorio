namespace Domain
{
    public class Region
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Region region &&
                   Name == region.Name;
        }
    }
}