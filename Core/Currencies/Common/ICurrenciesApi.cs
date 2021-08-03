using System;
using System.Threading.Tasks;

namespace Core.Currencies.Common
{
    public interface ICurrenciesApi
    {
        Task<CurrencyModel[]> GetCurrencies(DateTime? onDate = null);

        Task<CurrencyRateModel> GetCurrencyRate(string charCode, DateTime? onDate = null);

        Task<CurrencyRateModel[]> GetDynamics(string charCode, DateTime start, DateTime end);
    }
}
