using System;

namespace DataAccess.Entities
{
    public class Account
    {
        // global unique identifier
        public Guid Id { get; set; }

        public string CurrencyCharCode { get; set; }

        public decimal Amount { get; set; }

        public Guid UserId { get; set; }
    }
}
