var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseStatusCodePages();
app.UseMiddleware<RequestLoggingMiddle>();

app.UseMiddleware<KiemTraVeMiddleware>();

app.UseMiddleware<KiemTraNguoi>();
app.MapGet("/boing777", () => "san may tren troi");

app.Run();
