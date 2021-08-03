using System.Threading.Tasks;

namespace Core.Currencies.Common.Conversion
{
    public interface ICurrencyConversionService
    {
        Task<decimal> ConvertToLocal(decimal amount, string charCode);

        Task<decimal> ConvertFromLocal(decimal amount, string charCode);

        Task<decimal> Convert(string fromCharCode, string toCharCode, decimal amount);
    }
}
