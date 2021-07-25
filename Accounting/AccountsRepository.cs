using System;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess;
using DatabaseAccess.Entities;
using Microsoft.EntityFrameworkCore;

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

        public async Task Update(Account account, byte[] version)
        {
            _context.Entry(account).Property(nameof(account.Version)).OriginalValue = version;
            _context.Accounts.Update(account);
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

        public Task<Account[]> GetAll(string userId)
        {
            return _context.Accounts.Where(x => x.UserId == userId).ToArrayAsync();
        }
    }
}
