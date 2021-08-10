namespace DatabaseAccess.BookStore
{
    public class Letter
    {
        public int Id { get; set; }

        public char CharCode { get; set; }

        public string ColorCode { get; set; }

        public int PageId { get; set; }

        public Page Page { get; set; }
    }
}