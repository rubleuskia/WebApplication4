using System.Threading.Tasks;

namespace Core.Accounting
{
    public interface IAccountTransferService
    {
        Task Transfer(AccountTransferParameters parameters);
    }
}
