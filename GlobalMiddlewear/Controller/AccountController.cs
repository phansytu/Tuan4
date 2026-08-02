using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalMiddlewear.Dto;
using GlobalMiddlewear.Exceptions;
using GlobalMiddlewear.Models;
using GlobalMiddlewear.Service;
using Microsoft.AspNetCore.Mvc;

using GlobalMiddlewear.DataSource;
namespace GlobalMiddlewear.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            var account = await _accountService.CreateAccountAsync(request);
            return Ok(new
            {
                Success = true,
                Message = $"Tạo tài khoản thành công: {account.SoTaiKhoan} - {account.TenTaiKhoan} với số dư {account.SoDu:N0} VNĐ."
            });
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
        {
            var account = await _accountService.UpdateAccountAsync(request);
            return Ok(new
            {
                Success = true,
                Message = $"Cập nhật tài khoản thành công: {account.SoTaiKhoan} - {account.TenTaiKhoan} với số dư {account.SoDu:N0} VNĐ."
            });
        }
        [HttpGet("{soTaiKhoan}")]
        public async Task<IActionResult> GetAccount(string soTaiKhoan)
        {
            var account = await _accountService.GetAccountAsync(soTaiKhoan);
            return Ok(new
            {
                Success = true,
                Data = account
            });
        }
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = AccountData.accounts;
            return Ok(new
            {
                Success = true,
                Data = accounts
            });
        }
    }
}