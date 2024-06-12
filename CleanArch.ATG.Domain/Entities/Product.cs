
namespace CleanArch.ATG.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
    }
}
