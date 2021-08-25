using System.Threading.Tasks;

namespace DatabaseAccess.Infrastructure.Initializers
{
    public interface IDatabaseInitializer
    {
        Task Execute(ApplicationContext context);
    }
}