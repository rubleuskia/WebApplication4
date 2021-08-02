using System.Threading.Tasks;

namespace DatabaseAccess.Infrastructure.BeforeCommitHandlers
{
    public interface IBeforeCommitHandler
    {
        Task Execute(ApplicationContext context);
    }
}