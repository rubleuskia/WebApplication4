namespace Core.Accounting.Dtos
{
    public class CreateAccountDto
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}