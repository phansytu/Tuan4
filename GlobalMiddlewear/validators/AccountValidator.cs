using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using GlobalMiddlewear.Exceptions;
using GlobalMiddlewear.Models;
namespace GlobalMiddlewear.validators
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => x.SoTaiKhoan).NotEmpty().WithMessage("Số tài khoản không được để trống.")
            .Length(5, 10).WithMessage("Số tài khoản phải có độ dài 5 den 10 ký tự.");
            RuleFor(x => x.TenTaiKhoan).NotEmpty().WithMessage("Tên tài khoản không được để trống.")
            .MaximumLength(50).WithMessage("Tên tài khoản không được vượt quá 50 ký tự.");
            RuleFor(x => x.SoDu).GreaterThanOrEqualTo(0).WithMessage("Số dư phải lớn hơn hoặc bằng 0.");
        }

    }
}