namespace GlobalMiddlewear.Dto;

public record TransferRequest   //record dai dien cho du lieu client gui len API
(
    string tuTaikhoan,
    string DenTaiKhoan,
    decimal tienChuyen,
    string note);