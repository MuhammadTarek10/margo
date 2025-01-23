using Api.Responses;

using Domain.Exceptions;

namespace Api.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (NotFoundException notFound)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(
                    new ErrorResponse(
                        code: StatusCodes.Status404NotFound,
                        message: notFound.Message));
            logger.LogWarning(notFound.Message);
        }
        catch (ForbidException)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(
                    new ErrorResponse(
                        code: StatusCodes.Status403Forbidden,
                        message: "You are not authorized to access this resource."));
        }
        catch (AuthException ex)
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync(ex.Message);
        }
        catch (BadRequestException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(
                    new ErrorResponse(
                        code: StatusCodes.Status400BadRequest,
                        message: ex.Message));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(
                    new ErrorResponse(
                        code: StatusCodes.Status500InternalServerError,
                        message: $"Something went wrong [{ex.Message}]"));
            logger.LogError(ex.Message);
        }
    }
}