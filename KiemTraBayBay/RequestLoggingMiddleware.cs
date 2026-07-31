using System.Diagnostics;

public class RequestLoggingMiddle
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddle> _logger;

    public RequestLoggingMiddle(RequestDelegate request, ILogger<RequestLoggingMiddle> logger)
    {
        _next = request;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        //lay method
        var method = context.Request.Method;
        //lay url
        var url = context.Request.Path;
        //thoi gian bat dau
        var timer = Stopwatch.StartNew();

        try
        {
            await _next(context);
        }
        finally
        {
            //thoi gian dung chieu ve
            timer.Stop();
            _logger.LogInformation("method: {method}, Path: {Path}, time: {timer.ElapsedMilliseconds", method, url, timer.ElapsedMilliseconds);

        }
    }

}