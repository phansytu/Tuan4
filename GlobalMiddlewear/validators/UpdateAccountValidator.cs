using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GlobalMiddlewear.Dto;
using GlobalMiddlewear.Exceptions;
using GlobalMiddlewear.Models;
using FluentValidation;
namespace GlobalMiddlewear.validators
{
    public class UpdateAccountValidator : AbstractValidator<UpdateAccountRequest>
    {
        public UpdateAccountValidator()
        {
            RuleFor(x => x.SoTaiKhoan)
              .NotEmpty().WithMessage("Số tài khoản không được để trống.")
              .Length(5, 10).WithMessage("Số tài khoản phải có độ dài 5 den 10 ký tự.");
            RuleFor(x => x.TenTaiKhoan)
              .NotEmpty().WithMessage("Tên tài khoản không được để trống.")
              .MaximumLength(50).WithMessage("Tên tài khoản không được vượt quá 50 ký tự.");

        }
    }
}