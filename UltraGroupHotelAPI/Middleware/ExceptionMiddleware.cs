using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json;
using UltraGroupHotelAPI.API.Errors;
using UltraGroupHotelAPI.Application.Exceptions;

namespace UltraGroupHotelAPI.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context) {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);

                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = "";

                switch (ex)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var validationJson = JsonConvert.SerializeObject(validationException.Errors);
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode,ex.Message,validationJson));
                        break;

                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        break;
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, ex.Message, ex.StackTrace));
                }

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);
            }
        }
    }
}
