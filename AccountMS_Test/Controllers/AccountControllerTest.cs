using AccountMS.Controllers;
using AccountMS.Dtos;
using AccountMS.Models;
using AccountMS.Services.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AccountMS_Test.Controllers
{
    public class AccountControllerTest
    {
        AccountController target;
        Mock<IAccountService> _mockAccountService;
        public AccountControllerTest()
        {
            _mockAccountService = new Mock<IAccountService>();
            target = new AccountController(_mockAccountService.Object);
        }

        [Test]
        public void GetAccountsByUserId_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            //Arrange
            var listOfActiveAccounts = CreateResponseWithListOfAccounts();
            _mockAccountService
                .Setup(m => m.GetAccountsByUserId(1)).Returns(listOfActiveAccounts);

            //Act
            var res = target.GetAccountsByUserId(1);
            var OkResult = res as OkObjectResult; //Try to convert to type, if cannot convert return null.

            //Assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);
        }

        [Test]
        public void AddAccount_ShouldReturnBadRequestStatusCode_When_IsSuccessIsFalse()
        {
            //Arrange
            var AccountDto = CreateAccountDto();
            var response = new Response<object>();
            response.IsSuccess = false;

            _mockAccountService
                .Setup(m => m.AddAccount(AccountDto)).Returns(response);

            //Act
            var res = target.AddAccount(AccountDto);
            var badRequestResult = res as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void AddAccount_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            //Arrange
            var AccountDto = CreateAccountDto();
            var response = new Response<object>();
            response.IsSuccess = true;

            _mockAccountService
                .Setup(m => m.AddAccount(AccountDto)).Returns(response);

            //Act
            var res = target.AddAccount(AccountDto);
            var OkResult = res as OkObjectResult;

            //Assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);
        }

        [Test]
        public void UpdateAccountState_ShouldReturnBadRequestStatusCode_When_IsSuccessIsFalse()
        {
            //Arrange
            var response = new Response<object>();
            response.IsSuccess = false;

            _mockAccountService
                .Setup(m => m.UpdateAccountState(1, true)).Returns(response);

            //Act
            var res = target.UpdateAccountState(1, true);
            var badRequestResult = res as BadRequestObjectResult;

            //Assert
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public void UpdateAccountState_ShouldReturnOkStatusCode_When_IsSuccessIsTrue()
        {
            //Arrange
            var AccountDto = CreateAccountDto();
            var response = new Response<object>();
            response.IsSuccess = true;

            _mockAccountService
                .Setup(m => m.UpdateAccountState(1, true)).Returns(response);

            //Act
            var res = target.UpdateAccountState(1, true);
            var OkResult = res as OkObjectResult;

            //Assert
            Assert.IsNotNull(OkResult);
            Assert.AreEqual(200, OkResult.StatusCode);
        }

        private Response<List<Account>> CreateResponseWithListOfAccounts()
        {
            var data = new List<Account>();
            var response = new Response<List<Account>>();

            data.Add(new Account { Id = 1, UserId = 1, Active = true, CreatedDate = DateTime.Now });
            data.Add(new Account { Id = 2, UserId = 2, Active = true, CreatedDate = DateTime.Now });
            data.Add(new Account { Id = 3, UserId = 3, Active = true, CreatedDate = DateTime.Now });

            response.Data = data;
            response.IsSuccess = true;

            return response;
        }

        private AccountDto CreateAccountDto()
        {
            return new AccountDto() { UserId = 1};
        }
    }
}
