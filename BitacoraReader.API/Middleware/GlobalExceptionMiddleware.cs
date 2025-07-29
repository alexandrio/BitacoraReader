using BitacoraReader.API.Models;
using System.Net;
using System.Text.Json;

namespace BitacoraReader.API.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = new ApiResponse();

            switch (exception)
            {
                case ArgumentNullException:
                case ArgumentException:
                    response = ApiResponse.Error("Parámetros inválidos", new List<string> { exception.Message });
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case UnauthorizedAccessException:
                    response = ApiResponse.Error("No autorizado");
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case KeyNotFoundException:
                    response = ApiResponse.Error("Recurso no encontrado");
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case TimeoutException:
                    response = ApiResponse.Error("Tiempo de espera agotado");
                    context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    break;

                case OperationCanceledException:
                    response = ApiResponse.Error("Operación cancelada");
                    context.Response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    break;

                default:
                    response = ApiResponse.Error("Error interno del servidor");
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }

    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}