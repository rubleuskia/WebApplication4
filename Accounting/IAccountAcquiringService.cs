using System;
using System.Threading.Tasks;

namespace Accounting
{
    public interface IAccountAcquiringService
    {
        Task Withdraw(Guid accountId, byte[] rowVersion, decimal amount);
        Task Acquire(Guid accountId, byte[] rowVersion, decimal amount);
    }
}
