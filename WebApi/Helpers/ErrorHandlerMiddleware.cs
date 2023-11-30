namespace WebApi.Helpers;

using Gems.AplicationServices.Exceptions;
using Serilog;
using System.Net;
using System.Text.Json;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {

            var response = context.Response;
            response.ContentType = "application/json";

            switch (error)
            {
                case EntityNotFoundException e:
                    // custom application error
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                default:
                    // unhandled error
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = error?.Message });
            _logger.Error($"Error!: {result}");
            await response.WriteAsync(result);
        }
    }
}