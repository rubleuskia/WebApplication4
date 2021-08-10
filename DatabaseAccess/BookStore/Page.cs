using System.Collections.Generic;

namespace DatabaseAccess.BookStore
{
    public class Page
    {
        public int Id { get; set; }

        public int Number { get; set; }

        public Book Book { get; set; }

        public int BookId { get; set; }

        public ICollection<Letter> Lettes { get; set; }
    }
}