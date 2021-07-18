using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Contexts;
using DataAccess.Entities;

namespace Accounting
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ApplicationContext _context;

        public AccountsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Add(Account account)
        {
            if (_context.Accounts.All(x => x.Id != account.Id))
            {
                _context.Accounts.Add(account);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Update(Account account)
        {
            _context.Update(account);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid accountId)
        {
            Account result = await GetById(accountId);
            _context.Accounts.Remove(result);
            await _context.SaveChangesAsync();
        }

        public Task<Account> GetById(Guid accountId)
        {
            var account = _context.Accounts.SingleOrDefault(x => x.Id == accountId);
            if (account == null)
            {
                throw new InvalidOperationException("Cannot find an account with ID:" + accountId);
            }

            return Task.FromResult(account);
        }

        public Task<Account[]> GetAll()
        {
            return Task.FromResult(_context.Accounts.ToArray());
        }
    }
}
