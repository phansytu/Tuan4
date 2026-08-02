using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GlobalMiddlewear.Dto;
using GlobalMiddlewear.Exceptions;
using GlobalMiddlewear.Models;
using GlobalMiddlewear.DataSource;
namespace GlobalMiddlewear.Service
{
    public interface IAccountService
    {
        Task<Account> CreateAccountAsync(CreateAccountRequest request);
        Task<Account> UpdateAccountAsync(UpdateAccountRequest request);
        Task<Account> GetAccountAsync(string soTaiKhoan);
        Task<List<Account>> GetAllAccountsAsync();
    }
    public class AccountService : IAccountService
    {
        private readonly IValidator<CreateAccountRequest> _createValidator;
        private readonly IValidator<UpdateAccountRequest> _updateValidator;

        public AccountService(IValidator<CreateAccountRequest> createValidator, IValidator<UpdateAccountRequest> updateValidator)
        {
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }
        public async Task<Account> CreateAccountAsync(CreateAccountRequest request)
        {
            var validationResult = await _createValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var account = new Account
            {
                SoTaiKhoan = request.SoTaiKhoan,
                TenTaiKhoan = request.TenTaiKhoan,
                SoDu = request.SoDu
            };
            AccountData.accounts.Add(account);
            return account;
        }

        public async Task<Account> UpdateAccountAsync(UpdateAccountRequest request)
        {
            var validationResult = await _updateValidator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            lock (AccountData.accounts)
            {
                var account = AccountData.accounts.FirstOrDefault(a => a.SoTaiKhoan == request.SoTaiKhoan);
                if (account == null)
                {
                    throw new AccountNotFoundException(request.SoTaiKhoan);
                }

                account.TenTaiKhoan = request.TenTaiKhoan;
                account.SoDu = request.SoDu;
                return account;
            }

        }
        public async Task<Account> GetAccountAsync(string soTaiKhoan)
        {
            var account = AccountData.accounts.FirstOrDefault(a => a.SoTaiKhoan == soTaiKhoan);
            if (account == null)
            {
                throw new AccountNotFoundException(soTaiKhoan);
            }
            return account;
        }
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            return AccountData.accounts;
        }
    }
}