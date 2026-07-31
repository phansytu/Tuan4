using GlobalMiddlewear.Handler;
using GlobalMiddlewear.Service;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm Controllers và Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Đăng ký Dependency Injection & Exception Handler
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// 3. Bật Swagger giao diện UI
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