using GingerTemplate.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using ApplicationException = GingerTemplate.Core.Exceptions.ApplicationException;

namespace GingerTemplate.WebApi.Middleware;

/// <summary>
/// Middleware for handling exceptions globally and returning structured error responses.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            TraceId = context.TraceIdentifier,
            Timestamp = DateTime.UtcNow
        };

        switch (exception)
        {
            case ValidationException validationEx:
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                response.Code = "VALIDATION_ERROR";
                response.Message = validationEx.Message;
                break;

            case AuthenticationException authEx:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Code = "AUTHENTICATION_ERROR";
                response.Message = authEx.Message;
                break;

            case AuthorizationException authzEx:
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                response.Code = "AUTHORIZATION_ERROR";
                response.Message = authzEx.Message;
                break;

            case BusinessLogicException bizEx:
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                response.Code = "BUSINESS_LOGIC_ERROR";
                response.Message = bizEx.Message;
                break;

            case RepositoryException repoEx:
            case DatabaseException dbEx:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Code = "DATABASE_ERROR";
                response.Message = "An error occurred while accessing the database";
                break;

            case ExternalServiceException svcEx:
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                response.Code = "SERVICE_UNAVAILABLE";
                response.Message = svcEx.Message;
                break;

            case ApplicationException appEx:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Code = "APPLICATION_ERROR";
                response.Message = appEx.Message;
                break;

            default:
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Code = "INTERNAL_SERVER_ERROR";
                response.Message = "An unexpected error occurred";
                break;
        }

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(new { error = response }, options);

        return context.Response.WriteAsync(json);
    }
}

/// <summary>
/// Error response model for API errors.
/// </summary>
public class ErrorResponse
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string TraceId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
}
