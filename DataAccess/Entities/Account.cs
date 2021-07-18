using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Entities
{
    public class Account
    {
        // global unique identifier
        public Guid Id { get; set; }

        public string CurrencyCharCode { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        public Guid UserId { get; set; }
    }
}
