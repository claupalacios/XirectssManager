using AccountMS.Dtos;
using AccountMS.Models;
using Common.Models;
using System.Collections.Generic;

namespace AccountMS.Services.Interfaces
{
    public interface IAccountService
    {
        Response<List<Account>> GetAccountsByUserId(int userId);
        Response<object> AddAccount(AccountDto account);
        Response<object> UpdateAccountState(int accountId, bool active);

    }
}
