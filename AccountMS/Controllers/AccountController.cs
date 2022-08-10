using AccountMS.Dtos;
using AccountMS.Models;
using AccountMS.Services.Interfaces;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AccountMS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("accounts")]
        [ProducesResponseType(typeof(Response<List<Account>>), 200)]
        [ProducesResponseType(400)]
        public IActionResult GetAccountsByUserId(int userId)
        {
            var response = _accountService.GetAccountsByUserId(userId);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<object>), 200)]
        [ProducesResponseType(400)]
        public IActionResult AddAccount(AccountDto account)
        {
            var response = _accountService.AddAccount(account);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpPatch]
        [ProducesResponseType(typeof(Response<object>), 200)]
        [ProducesResponseType(400)]
        public IActionResult UpdateAccountState(int accountId, bool active)
        {
            var response = _accountService.UpdateAccountState(accountId, active);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}
