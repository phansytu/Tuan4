using Microsoft.AspNetCore.Http;

public class KiemTraNguoi
{
    private readonly RequestDelegate _next;

    public KiemTraNguoi(RequestDelegate request)
    {


        _next = request;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Bao Ve dang kiem tra nguoi...");
        await Task.Delay(1000);
        Console.WriteLine("Kiem Tra nguoi xong, ket Qua kiem tra, doi trong it giay");
        await Task.Delay(1500);
        int randomNumber = Random.Shared.Next(1, 101);
        var soichieu = randomNumber <= 80;
        if (!soichieu)
        {
            context.Response.StatusCode = 403;
            Console.WriteLine("Xich co lai, di tu nha con");
            return;
        }
        else
        {
            Console.WriteLine("An toan, cho len may bay");
            await _next(context);
        }

    }
}