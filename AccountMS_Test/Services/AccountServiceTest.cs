using AccountMS.Dtos;
using AccountMS.Helpers;
using AccountMS.Models;
using AccountMS.Repositories.Interfaces;
using AccountMS.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AccountMS_Test.Services
{
    public class AccountServiceTest
    {
        AccountService target;
        IMapper _mapper;
        Mock<IAccountRepository> _mockAccountRepository;
        Mock<ILogger<AccountService>> _mockLogger;
        public AccountServiceTest()
        {
            SetupMapper();
            _mockAccountRepository = new Mock<IAccountRepository>();
            _mockLogger = new Mock<ILogger<AccountService>>();

            target = new AccountService(_mockAccountRepository.Object, _mapper, _mockLogger.Object);
        }

        private void SetupMapper()
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(new Profile[] {
                        new MappingProfile()
                    });
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Test]
        public void GetAllActiveAccounts_ShouldReturnIsSuccessTrue_AndReturnListOfAccounts_When_ListOfAccountsIsFound()
        {
            //Arrange
            var listOfAccounts = CreateListOfAccounts();

            _mockAccountRepository
                .Setup(m => m.GetAccountsByUserId(1)).Returns(listOfAccounts);

            //Act
            var res = target.GetAccountsByUserId(1);

            //Assert
            Assert.AreEqual(res.IsSuccess, true);
            Assert.AreEqual(res.Data, listOfAccounts);
        }

        [Test]
        public void GetAllActiveAccounts_ShouldReturnIsSuccessFalse_When_ListOfAccountsIsNotFound()
        {
            //Arrange

            _mockAccountRepository
                .Setup(m => m.GetAccountsByUserId(1)).Returns<List<Account>>(null);

            //Act
            var res = target.GetAccountsByUserId(1);

            //Assert
            Assert.AreEqual(res.IsSuccess, false);
        }

        [Test]
        public void AddAccount_ShouldAddAccount_When_DataIsOk()
        {
            //Arrange
            var AccountDto = CreateAccountDto();
            var Account = CreateAccount();

            _mockAccountRepository
                .Setup(m => m.AddAccount(Account));

            //Act
            var res = target.AddAccount(AccountDto);

            //Assert
            Assert.AreEqual(res.IsSuccess, true);
        }

        [Test]
        public void UpdateAccountState_ShouldReturnIsSuccessTrue_When_IdExist()
        {
            //Arrange
            var AccountId = 1;
            var AccountState = true;
            var Account = CreateAccount();

            _mockAccountRepository
                .Setup(m => m.UpdateAccountState(AccountId, AccountState)).Returns(Account);

            //Act
            var res = target.UpdateAccountState(AccountId, AccountState);

            //Assert
            Assert.AreEqual(res.IsSuccess, true);
        }

        [Test]
        public void UpdateAccountState_ShouldReturnIsSuccessFalse_When_IdNotExist()
        {
            //Arrange
            var AccountId = 2;
            var AccountState = true;
            var Account = CreateAccount();

            _mockAccountRepository
                .Setup(m => m.UpdateAccountState(AccountId, AccountState)).Returns<Account>(null);

            //Act
            var res = target.UpdateAccountState(AccountId, AccountState);

            //Assert
            Assert.AreEqual(res.IsSuccess, false);
        }

        private List<Account> CreateListOfAccounts()
        {
            var listOfAccounts = new List<Account>();

            listOfAccounts.Add(new Account { Id = 1, UserId = 1, Active = true, CreatedDate = DateTime.Now });
            listOfAccounts.Add(new Account { Id = 2, UserId = 2, Active = true, CreatedDate = DateTime.Now });
            listOfAccounts.Add(new Account { Id = 3, UserId = 3, Active = true, CreatedDate = DateTime.Now });

            return listOfAccounts;
        }

        private AccountDto CreateAccountDto()
        {
            return new AccountDto() { UserId = 1 };
        }

        private Account CreateAccount()
        {
            return new Account() { Id = 3, UserId = 3, Active = true, CreatedDate = DateTime.Now };
        }
    }
}
