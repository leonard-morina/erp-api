using Erp.Core.Error;

namespace Erp.Api.Middlewares;

public class ExceptionWithErrorCodeHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionWithErrorCodeHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ExceptionWithErrorCode ex)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new {errorCode = ex.Message});
        }
    }
}