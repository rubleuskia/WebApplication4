using System;

namespace Core.Accounting.Dtos
{
    public class AccountDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyName { get; set; }
    }
}