namespace CleanArch.ATG.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
    }
}