using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace StatementGeneratorService.Api.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            var req = context.Request;
            _logger.LogInformation("Incoming request {Method} {Path}", req.Method, req.Path);

            await _next(context);

            sw.Stop();
            var status = context.Response?.StatusCode;
            _logger.LogInformation("Request {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds}ms", req.Method, req.Path, status, sw.ElapsedMilliseconds);
        }
    }
}
