using AccountMS.Dtos;
using AccountMS.Models;
using AccountMS.Repositories.Interfaces;
using AccountMS.Services.Interfaces;
using AutoMapper;
using Common.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace AccountMS.Services
{
    public class AccountService : IAccountService
    {
        private readonly string DataNotValid = "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException";
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository accountRepository, IMapper mapper, ILogger<AccountService> logger)
        {
            _accountRepository = accountRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Return all the accounts from a given user
        /// </summary>
        /// <returns>List of accounts from the user</returns>
        public Response<List<Account>> GetAccountsByUserId(int userId)
        {
            var response = new Response<List<Account>>();

            try
            {
                _logger.LogInformation("AccountService - Getting accounts");
                var result = _accountRepository.GetAccountsByUserId(userId);
                if (result != null)
                {
                    response.Data = result;
                    response.IsSuccess = true;
                    response.Message = $"{result.Count} account/s were found.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("AccountService - The following error occurs: {@ex}", ex);
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// Insert a new Account in the database
        /// </summary>
        /// <param name="account">Account to insert</param>
        /// <returns>Object with information</returns>
        public Response<object> AddAccount(AccountDto account)
        {
            var response = new Response<object>();

            try
            {
                _logger.LogInformation("AccountService - Adding new account");
                var accountToAdd = _mapper.Map<Account>(account);
                accountToAdd.CreatedDate = DateTime.Now;

                _accountRepository.AddAccount(accountToAdd);

                response.Data = account;
                response.IsSuccess = true;
                response.Message = $"Account was added successfully.";
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() == DataNotValid)
                {
                    response.IsSuccess = false;
                    response.Message = $"Account was not added because user with Id {account.UserId} does not exist";
                    return response;
                }

                _logger.LogInformation("AccountService - The following error occurs: {@ex}", ex);
            }

            return response;
        }

        /// <summary>
        /// Update the Active property of a given account
        /// </summary>
        /// <param name="accountId">Id of the account to update</param>
        /// <param name="active">State of the account</param>
        /// <returns>Object with information</returns>
        public Response<object> UpdateAccountState(int accountId, bool active)
        {
            var response = new Response<object>();
            try
            {
                _logger.LogInformation("AccountService - Updating account state");
                var result = _accountRepository.UpdateAccountState(accountId, active);
                if (result != null)
                {
                    if (result.Active == active)
                    {
                        response.Data = accountId;
                        response.IsSuccess = true;
                        response.Message = $" Account with Id: {accountId} updated successfully.";
                    }
                }
                else
                {
                    response.Message = $" Account with Id: {accountId} could not be updated.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("AccountService - The following error occurs: {@ex}.", ex);
            }

            return response;
        }
    }
}
