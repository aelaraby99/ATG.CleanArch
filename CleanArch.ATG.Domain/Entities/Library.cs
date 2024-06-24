using System.Collections.ObjectModel;

namespace CleanArch.ATG.Domain.Entities
{
    public class Library
    {
        public int LibraryId { get; set; }
        public string LibraryName { get; set; }
        public string Location { get; set; }
        //public ICollection<Book>? Books { get; set; }
    }
}