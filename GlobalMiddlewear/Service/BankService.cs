using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalMiddlewear.Dto;
using GlobalMiddlewear.Exceptions;
using GlobalMiddlewear.Models;

namespace GlobalMiddlewear.Service
{
    public interface IBankService
    {
        Task TransferAsync(TransferRequest request);
    }

    public class BankService : IBankService
    {
        public readonly List<Account> accounts = new()
        {
            new Account { SoTaiKhoan = "9999", TenTaiKhoan = "Phan Sy Tu", SoDu = 100000000m },
            new Account { SoTaiKhoan = "9998", TenTaiKhoan = "Anh La Tu", SoDu = 1500000m }
        };

        public async Task TransferAsync(TransferRequest request)
        {
            //neu nhu trung so tai khoan cua nguoi gui
            if (request.tuTaikhoan == request.DenTaiKhoan)
            {
                throw new InvalidTransferException("Tài khoản nguồn và tài khoản đích không được trùng nhau");
            }

            //so tien chuyen phai lon hon 0
            if (request.tienChuyen <= 0)
            {
                throw new InvalidTransferException("Số tiền chuyển phải lớn hơn 0 VNĐ.");
            }

            //nguoi chuyen khong duoc trung nguoi nhan
            var fromAccount = accounts.FirstOrDefault(a => a.SoTaiKhoan == request.tuTaikhoan)
                ?? throw new AccountNotFoundException(request.tuTaikhoan);

            var toAccount = accounts.FirstOrDefault(a => a.SoTaiKhoan == request.DenTaiKhoan)
                ?? throw new AccountNotFoundException(request.DenTaiKhoan);

            if (fromAccount.SoDu < request.tienChuyen)
                throw new InsufficientBalanceException(fromAccount.SoTaiKhoan, fromAccount.SoDu, request.tienChuyen);

            // thuc hien chuyen tien
            await Task.Delay(100); // do tre
            fromAccount.SoDu -= request.tienChuyen;
            toAccount.SoDu += request.tienChuyen;
        }
    }
}