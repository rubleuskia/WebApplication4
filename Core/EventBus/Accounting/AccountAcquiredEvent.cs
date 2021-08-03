using System;

namespace Core.EventBus.Accounting
{
    public class AccountAcquiredEvent : IEvent
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
