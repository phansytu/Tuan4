using GlobalMiddlewear.Handler;
using GlobalMiddlewear.Service;
using GlobalMiddlewear.validators;
using FluentValidation;

namespace GlobalMiddlewear;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddValidatorsFromAssemblyContaining<Program>();


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IBankService, BankService>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // 4. Kích hoạt Exception Handler Middleware
        app.UseExceptionHandler();

        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}