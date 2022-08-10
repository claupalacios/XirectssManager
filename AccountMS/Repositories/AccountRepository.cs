using AccountMS.Models;
using AccountMS.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AccountMS.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountDBContext _context;

        public AccountRepository(AccountDBContext context)
        {
            _context = context;
        }

        public List<Account> GetAccountsByUserId(int userId)
        {
            return _context.Accounts.Where(x => x.UserId == userId).ToList();
        }

        public void AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public Account UpdateAccountState(int accountId, bool active)
        {
            var accountToUpdate = _context.Accounts.Find(accountId);
            if (accountToUpdate != null)
            {
                _context.Accounts.Attach(accountToUpdate);
                accountToUpdate.Active = active;
                _context.Entry(accountToUpdate).Property(x => x.Active).IsModified = true;
                _context.SaveChanges();
                return _context.Accounts.Find(accountId);
            }
            return null;
        }

        public Account GetAccountById(int accountId)
        {
            var account = _context.Accounts.Find(accountId);
            if (account != null)
            {
                return account;
            }
            else
            {
                return null;
            }
        }
    }
}
