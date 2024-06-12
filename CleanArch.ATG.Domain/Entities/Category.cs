namespace CleanArch.ATG.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BrandCategory> BrandCategories { get; set; }
    }
}