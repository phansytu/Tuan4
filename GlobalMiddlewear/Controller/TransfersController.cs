using System.Threading.Tasks;
using GlobalMiddlewear.Dto;
using GlobalMiddlewear.Service;
using Microsoft.AspNetCore.Mvc;

namespace GlobalMiddlewear.Controller;

[ApiController]
[Route("api/[controller]")]
public class TransfersController : ControllerBase
{
    private readonly IBankService _bankService;

    public TransfersController(IBankService bankService)
    {
        _bankService = bankService;
    }

    [HttpPost]
    public async Task<IActionResult> Transfer([FromBody] TransferRequest request)
    {
        await _bankService.TransferAsync(request);
        return Ok(new
        {
            Success = true,
            Message = $"Chuyển thành công {request.tienChuyen:N0} VNĐ từ tài khoản {request.tuTaikhoan} sang {request.DenTaiKhoan}."
        });
    }
}