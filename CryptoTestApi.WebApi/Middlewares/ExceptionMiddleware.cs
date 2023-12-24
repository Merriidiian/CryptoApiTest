using System.Net;
using FluentValidation;

namespace CryptoTestApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<ExceptionMiddleware> logger)
    {
        logger.LogInformation("Handling {path}", context.Request.Path.Value);
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new ErrorDto(string.Join("\n", ex.Errors.Select(x => x.ErrorMessage))));
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Unhandled Exception");
            context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
            await context.Response.WriteAsJsonAsync(new ErrorDto("Непредвиденная ошибка"));
        }
    }
}