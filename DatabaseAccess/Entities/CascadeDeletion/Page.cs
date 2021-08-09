using System.Collections.Generic;

namespace DatabaseAccess.Entities.CascadeDeletion
{
    public class Page
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }

        public ICollection<Letter> Letters { get; set; }
    }
}
