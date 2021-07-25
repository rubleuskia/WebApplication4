using System;

namespace Accounting
{
    public class AccountTransferParameters
    {
        public Guid FromAccount { get; set; }

        public byte[] FromVersion { get; set; }

        public Guid ToAccount { get; set; }

        public byte[] ToVersion { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCharCode { get; set; }
    }
}
