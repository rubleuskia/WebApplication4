using System;

namespace Core.EventBus.Accounting
{
    public class AccountWithdrawEvent : IEvent
    {
        public Guid AccountId { get; set; }

        public decimal Amount { get; set; }
    }
}
