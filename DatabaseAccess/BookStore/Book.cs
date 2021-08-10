using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseAccess.BookStore
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Page> Pages { get; set; }
    }
}