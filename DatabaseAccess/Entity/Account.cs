using System;

namespace DatabaseAccess.Entity
{
    public class Account
    {
        public Guid Id { get; set; }

        public string CurrencyCharCode { get; set; }

        public decimal Amount { get; set; }

        public Guid UserId { get; set; }
    }
}
