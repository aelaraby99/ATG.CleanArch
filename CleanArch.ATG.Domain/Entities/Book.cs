using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        //public decimal Price { get; set; }
        //public string Price { get; set; }
        //public int LibraryId { get; set; }
        //public Library Library { get; set; }
    }
}
