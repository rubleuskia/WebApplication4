namespace DatabaseAccess.Entities
{
    public class Account : BaseEntity
    {
        public string CurrencyCharCode { get; set; }

        public decimal Amount { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public byte[] Version { get; set; }
    }
}
