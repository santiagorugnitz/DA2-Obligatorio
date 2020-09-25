namespace Domain
{
    public class Category
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Category category &&
                   Name == category.Name;
        }
    }
}