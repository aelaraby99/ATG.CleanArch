
namespace CleanArch.ATG.Domain.Entities
{
    public class BaseEntity
    {
        public byte IsDeleted { get; set; }= default;
        public DateTime CreationDate { get; set; }
    }
}
