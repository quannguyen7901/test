using Demo.Common.Execption;
using System.Net;

namespace Demo.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private async Task HandleExceptionAsync(HttpContext context,Exception exception)
        {
            context.Response.ContentType = "application/json";
            if(exception is NotFoundException)
            { 
                context.Response.StatusCode= ((NotFoundException)exception).ErrorCode;
                await context.Response.WriteAsync(new BaseException()
                {
                    ErrorCode = ((NotFoundException)exception).ErrorCode,
                    Message = "Không tìm được bản ghi",
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink
                }.ToString());
            }
            else if (exception is InternalException)
            {
                context.Response.StatusCode = ((InternalException)exception).ErrorCode;
                await context.Response.WriteAsync(new BaseException()
                {
                    ErrorCode = ((InternalException)exception).ErrorCode,
                    Message = "Lỗi hệ thống",
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink
                }.ToString());
            }
            else if (exception is WrongFormatException)
            {

                context.Response.StatusCode = ((WrongFormatException)exception).ErrorCode;
                await context.Response.WriteAsync(new BaseException()
                {
                    ErrorCode = ((WrongFormatException)exception).ErrorCode,
                    Message = exception.Message,
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink
                }.ToString());
            }
            else if (exception is NotBuildException)
            {

                context.Response.StatusCode = ((NotBuildException)exception).ErrorCode;
                await context.Response.WriteAsync(new BaseException()
                {
                    ErrorCode = ((NotBuildException)exception).ErrorCode,
                    Message = "Chức năng tạm thời chưa xây dựng",
                    DevMessage = exception.Message,
                    TraceId = context.TraceIdentifier,
                    MoreInfo = exception.HelpLink
                }.ToString());
            }
        }
    }
}
