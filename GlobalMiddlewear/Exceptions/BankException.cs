using System;

namespace GlobalMiddlewear.Exceptions
{
    public class BankException : Exception
    {
        public BankException(string message) : base(message) { }
    }

    //không tìm thấy tài khoản (404)
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException(string SoTaiKhoan)
            : base($"Không tìm thấy tài khoản số: {SoTaiKhoan}") { }
    }

    //không đủ số dư (400 or 422)
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException(string SoTaiKhoan, decimal SoDu, decimal tienChuyen)
            : base($"Tài khoản {SoTaiKhoan} không đủ số dư. Số dư hiện tại: {SoDu:N0} VNĐ, số tiền cần chuyển: {tienChuyen:N0} VNĐ.") { }
    }


    //quy tắc chuyển tiền (400)
    public class InvalidTransferException : Exception
    {
        public InvalidTransferException(string message) : base(message) { }
    }
    public class AccountAlreadyExistsException : BankException
    {
        public AccountAlreadyExistsException(string soTaiKhoan)
            : base($"Tài khoản số {soTaiKhoan} đã tồn tại trong hệ thống.") { }
    }
}