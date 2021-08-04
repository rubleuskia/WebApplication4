using System;

namespace Core.Accounting.Exceptions
{
    public class NotEnoughMoneyToWithdrawException : Exception
    {
        public Guid AccountId { get; set; }

        public decimal OriginalAmount { get; set; }

        public decimal Amount { get; set; }

        public NotEnoughMoneyToWithdrawException(string message) : base(message)
        {
        }
    }
}
