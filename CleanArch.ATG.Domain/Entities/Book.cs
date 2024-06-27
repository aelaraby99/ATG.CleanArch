using System.ComponentModel.DataAnnotations;

namespace CleanArch.ATG.Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        //public decimal Price { get; set; }
        //public int AccNumber { get; set; }
        //public string Price { get; set; }
        //public int LibraryId { get; set; }
        //public Library Library { get; set; }
    }
}
