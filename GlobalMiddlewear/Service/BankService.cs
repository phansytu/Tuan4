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
    public interface IBankService
    {
        Task TransferAsync(TransferRequest request);
    }
    public class BankService : IBankService
    {
        private readonly IValidator<TransferRequest> _validator;

        public BankService(IValidator<TransferRequest> validator)
        {
            _validator = validator;
        }

        public async Task TransferAsync(TransferRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new InvalidTransferException("Dữ liệu chuyển khoản không hợp lệ.");
            }
            lock (AccountData.accounts)
            {
                var fromAccount = AccountData.accounts.FirstOrDefault(a => a.SoTaiKhoan == request.tuTaikhoan)
                    ?? throw new AccountNotFoundException(request.tuTaikhoan);

                var toAccount = AccountData.accounts.FirstOrDefault(a => a.SoTaiKhoan == request.DenTaiKhoan)
                    ?? throw new AccountNotFoundException(request.DenTaiKhoan);

                if (fromAccount.SoDu < request.tienChuyen)
                    throw new InsufficientBalanceException(fromAccount.SoTaiKhoan, fromAccount.SoDu, request.tienChuyen);

                // thuc hien chuyen tien
                fromAccount.SoDu -= request.tienChuyen;
                toAccount.SoDu += request.tienChuyen;
            }

            // thuc hien chuyen tien
            await Task.Delay(100); // do tre

        }
    }
}