using System;

namespace DatabaseAccess.Entities
{
    public class Account : BaseEntity
    {
        public Guid Id { get; set; }

        public string CurrencyCharCode { get; set; }

        public decimal Amount { get; set; }

        // OwnedById
        public string UserId { get; set; }

        // navigation property
        public User User { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
