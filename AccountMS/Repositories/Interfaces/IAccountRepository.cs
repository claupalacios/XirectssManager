using AccountMS.Models;
using System.Collections.Generic;

namespace AccountMS.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public List<Account> GetAccountsByUserId(int userId);
        public void AddAccount(Account account);
        public Account UpdateAccountState(int accountId, bool active);
        public Account GetAccountById(int accountId);
    }
}
