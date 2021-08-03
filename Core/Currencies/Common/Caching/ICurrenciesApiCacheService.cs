using System.Threading.Tasks;

namespace Core.Currencies.Common.Caching
{
    public interface ICurrenciesApiCacheService : ICurrenciesApi
    {
        Task Initialize();
    }
}
