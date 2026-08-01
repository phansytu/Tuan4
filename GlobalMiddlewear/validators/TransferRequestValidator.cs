using FluentValidation;
using GlobalMiddlewear.Dto;

namespace GlobalMiddlewear.Validators;

public class TransferRequestValidator : AbstractValidator<TransferRequest>
{
    public TransferRequestValidator()
    {
        RuleFor(x => x.tuTaikhoan)
            .NotEmpty().WithMessage("Số tài khoản nguồn không được để trống.");

        RuleFor(x => x.DenTaiKhoan)
            .NotEmpty().WithMessage("Số tài khoản đích không được để trống.");

        RuleFor(x => x.tienChuyen)
            .GreaterThan(0).WithMessage("Số tiền chuyển phải lớn hơn 0 VNĐ.");

        RuleFor(x => x)
            .Must(x => x.tuTaikhoan != x.DenTaiKhoan)
            .WithMessage("Tài khoản nguồn và tài khoản đích không được trùng nhau.");
    }
}