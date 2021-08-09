using System;

namespace DatabaseAccess.Entities.CascadeDeletion
{
    public class Page
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid BookId { get; set; }

        public Book Book { get; set; }
    }
}
