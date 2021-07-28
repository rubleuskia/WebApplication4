using System.Threading.Tasks;

namespace DatabaseAccess.Infrastructure
{
    public interface IAsyncBeforeCommitHandler
    {
        Task Execute(ApplicationContext context);
    }
}