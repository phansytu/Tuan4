namespace GlobalMiddlewear.Dto;

public record TransferRequest   //record dai dien cho du lieu client gui len API
(
    string tuTaikhoan,
    string DenTaiKhoan,
    decimal tienChuyen,
    string note);

public record CreateAccountRequest
(
    string SoTaiKhoan,
    string TenTaiKhoan,
    decimal SoDu);
public record UpdateAccountRequest
(
    string SoTaiKhoan,
    string TenTaiKhoan,
    decimal SoDu);