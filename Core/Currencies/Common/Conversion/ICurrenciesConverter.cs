namespace Core.Currencies.Common.Conversion
{
    public interface ICurrenciesConverter
    {
        double ConvertToLocal(double amount, CurrencyRateModel rate);
        double ConvertFromLocal(double amount, CurrencyRateModel rate);
    }
}
