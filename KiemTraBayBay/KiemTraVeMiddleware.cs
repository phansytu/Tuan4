
using System.Net.Cache;

public class KiemTraVeMiddleware
{
    private readonly RequestDelegate _next; //requestDelegate la con tro ham, dung de lay middleware sau dua vao constructor o truoc

    public KiemTraVeMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Nhan vien dang kiem tra ve....");
        await Task.Delay(2000);
        Console.WriteLine("Nhan vien kiem tra ve thanh cong, dang dua ra ket qua..");
        await Task.Delay(1000);
        int randomNumber = Random.Shared.Next(1, 101);
        bool ve = randomNumber <= 70;

        if (!ve)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Tram 1: fasle");
            return;
        }
        else
        {
            Console.WriteLine("hop le!, chuyen sang tram 2 ktra tiep theo");
            await _next(context);
        }
    }

}