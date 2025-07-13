//using FluentValidation;
//using HRMS.Application.Common.Exceptions;
//using System.Net;
//using System.Text.Json;

//namespace HRMS.API.MiddleWare
//{
//    public class ExceptionHandlingMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

//        public ExceptionHandlingMiddleware(
//            RequestDelegate next,
//            ILogger<ExceptionHandlingMiddleware> logger)
//        {
//            _next = next;
//            _logger = logger;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {
//                await HandleExceptionAsync(context, ex);
//            }
//        }

//        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
//        {
//            _logger.LogError(exception, "An error occurred");

//            HttpStatusCode statusCode;
//            string title;
//            object details = exception.Message; // Default to exception message

//            switch (exception)
//            {
//                case ValidationException validationEx:
//                    statusCode = HttpStatusCode.BadRequest;
//                    title = "Validation Error";
//                    details = HandleValidationError(validationEx);
//                    break;

//                case NotFoundException:
//                    statusCode = HttpStatusCode.NotFound;
//                    title = "Resource Not Found";
//                    break;

//                case BadRequestException:
//                    statusCode = HttpStatusCode.BadRequest;
//                    title = "Business Rule Violation";
//                    break;

//                case UnauthorizedAccessException:
//                    statusCode = HttpStatusCode.Unauthorized;
//                    title = "Access Denied";
//                    details = "You don't have permission to access this resource";
//                    break;

//                default:
//                    statusCode = HttpStatusCode.InternalServerError;
//                    title = "Server Error";
//                    details = "An unexpected error occurred";
//                    break;
//            }

//            context.Response.ContentType = "application/json";
//            context.Response.StatusCode = (int)statusCode;

//            await context.Response.WriteAsync(JsonSerializer.Serialize(new
//            {
//                Status = statusCode,
//                Title = title,
//                Details = details,
//                Timestamp = DateTime.UtcNow
//            }));


//        }

//        private static IDictionary<string, string[]> HandleValidationError(ValidationException ex)
//        {
//            return ex.Errors
//                .GroupBy(x => x.PropertyName)
//                .ToDictionary(
//                    g => g.Key,
//                    g => g.Select(x => x.ErrorMessage).ToArray()
//                );
//        }
//    }
//}
using FluentValidation;
using HRMS.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace HRMS.API.MiddleWare
{
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred while processing the request.");

            HttpStatusCode statusCode;
            string title;
            object details = exception.Message;

            switch (exception)
            {
                case ValidationException validationEx:
                    statusCode = HttpStatusCode.BadRequest;
                    title = "Validation Error";
                    details = HandleValidationError(validationEx);
                    break;

                case NotFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    title = "Resource Not Found";
                    break;

                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    title = "Business Rule Violation";
                    break;

                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    title = "Access Denied";
                    details = "You don't have permission to access this resource.";
                    break;

                case ForbiddenException:
                    statusCode = HttpStatusCode.Forbidden;
                    title = "Forbidden";
                    details = "You are not allowed to access this resource.";
                    break;

                case ServiceUnavailableException:
                    statusCode = HttpStatusCode.ServiceUnavailable;
                    title = "Service Unavailable";
                    details = "The service is currently unavailable. Please try again later.";
                    break;

                case BadGatewayException:
                    statusCode = HttpStatusCode.BadGateway;
                    title = "Bad Gateway";
                    details = "There was an issue communicating with an upstream server.";
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    title = "Server Error";
                    details = "An unexpected error occurred.";
                    break;

                case InsufficientStockException:
                    statusCode = HttpStatusCode.BadRequest;
                    title = "Insufficient Stock";
                    details = exception.Message;
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                status = (int)statusCode,
                title,
                details,
                timestamp = DateTime.UtcNow
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }

        private static IDictionary<string, string[]> HandleValidationError(ValidationException ex)
        {
            return ex.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray()
                );
        }
    }
    /// this is for 200,301,302,400,403,404,500,502,503
  


   

   
}







