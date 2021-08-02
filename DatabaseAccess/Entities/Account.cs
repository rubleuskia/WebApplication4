using DatabaseAccess.Entities.Common;

namespace DatabaseAccess.Entities
{
    public class Account : ApplicationEntity, IHaveVersion
    {
        public string CurrencyCharCode { get; set; }

        public decimal Amount { get; set; }

        public string UserId { get; set; }

        // navigation property
        public User User { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
