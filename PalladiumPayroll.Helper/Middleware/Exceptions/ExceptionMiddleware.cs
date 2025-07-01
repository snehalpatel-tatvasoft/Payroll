using Microsoft.AspNetCore.Http;
using PalladiumPayroll.Helper.Log4net;
using PalladiumPayroll.Helper.Middleware.Exceptions.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace PalladiumPayroll.Helper.Middleware.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _nextMiddleware;
        protected readonly ILog4net _ILog4net;
        public ExceptionMiddleware(RequestDelegate nextMiddleware, ILog4net ILog4net)
        {
            _nextMiddleware = nextMiddleware;
            _ILog4net = ILog4net;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _nextMiddleware(context);
            }
            catch (Exception ex)
            {
                await HandleCustomExceptionResponseAsync(context, ex);
            }
        }
        private async Task HandleCustomExceptionResponseAsync(HttpContext context, Exception exception)
        {
            ErrorDetails errorDetails = new ErrorDetails();
            var statusCode = HttpStatusCode.InternalServerError; // 500 if unexpected
            var message = exception.Message;
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case NotImplementedException notImplementedException:
                    statusCode = HttpStatusCode.NotImplemented;
                    errorDetails.Message = exception.Message;
                    break;
                case UnauthorizedAccessException unauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorDetails.Message = exception.Message;
                    break;
                case BusinessRuleException businessRuleException:
                    statusCode = HttpStatusCode.UnprocessableEntity;
                    errorDetails.Message = exception.Message;
                    break;
                case NoContentException noContentException:
                    statusCode = HttpStatusCode.NoContent;
                    break;
                default:
                    errorDetails.Message = "An error occurred on the server";
                    break;
            }
            context.Response.StatusCode = errorDetails.StatusCode = Convert.ToInt32(statusCode);
            if (context.Response.StatusCode == 204)
            {
                return;
            }

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(errorDetails, options);
            LoggingError(exception);
            await context.Response.WriteAsync(json);
        }
        public void LoggingError(Exception ex)
        {
            string source = ex.Source;
            string stack = ex.StackTrace;
            string message = ex.InnerException != null ? string.Format("{0}: {1}", ex.Message, ex.InnerException.ToString()) : ex.Message;

            _ILog4net.Error(source + "( Unhandled Exception )" + message + stack);

        }
    }
}
