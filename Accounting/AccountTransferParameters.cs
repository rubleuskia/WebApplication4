using System;

namespace Accounting
{
    public class AccountTransferParameters
    {
        public Guid FromAccount { get; set; }

        public Guid ToAccount { get; set; }

        public decimal Amount { get; set; }

        public string CurrencyCharCode { get; set; }

        public byte[] FromRowVersion { get; set; }
        public byte[] ToRowVersion { get; set; }
    }
}
