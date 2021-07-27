using System.Threading.Tasks;

namespace DatabaseAccess.Infrastructure
{
    public interface IBeforeCommitHandler
    {
        Task Execute(ApplicationContext context);
    }
}