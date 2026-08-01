using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using GlobalMiddlewear.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace GlobalMiddlewear.Handler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Lỗi giao dịch ngân hàng: {Message}", exception.Message);
            if (exception is ValidationException validationException)
            {
                var errors = validationException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
                var ValidationProblemDetails = new HttpValidationProblemDetails(errors)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Dữ liệu không hợp lệ",
                    Detail = "Vui lòng kiểm tra lại dữ liệu gửi lên.",
                    Instance = httpContext.Request.Path
                };

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(ValidationProblemDetails, cancellationToken);

                return true;
            }

            // var (statusCode, title):     
            var (statusCode, title) = exception switch //Lấy giá trị của biến exception, kiểm tra nó thuộc loại nào, sau đó trả về một kết quả tương ứng.
            {
                AccountNotFoundException => (StatusCodes.Status404NotFound, "Tài khoản không tồn tại"),
                InsufficientBalanceException => (StatusCodes.Status400BadRequest, "Số dư không đủ"),
                InvalidTransferException => (StatusCodes.Status400BadRequest, "Giao dịch không hợp lệ"),
                _ => (StatusCodes.Status500InternalServerError, "Lỗi hệ thống ngân hàng") //moi truong hop con lai
            };

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}