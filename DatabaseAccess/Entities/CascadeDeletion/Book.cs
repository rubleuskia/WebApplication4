using System.Collections.Generic;

namespace DatabaseAccess.Entities.CascadeDeletion
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public ICollection<Page> Pages { get; set; }
    }
}
