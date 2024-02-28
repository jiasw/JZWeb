using JZ.Application.Contract.Dtos;

namespace JZ.WebAPI.Common
{
    /// <summary>
    /// 全局异常捕获
    /// </summary>
    public class ExceptionHandlerExtensions
    {
        private readonly RequestDelegate next;
        private ILogger<ExceptionHandlerExtensions> logger;
        public ExceptionHandlerExtensions(RequestDelegate next, ILogger<ExceptionHandlerExtensions> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            //记日志
            logger.LogError($"堆栈信息：{e.StackTrace},异常描述：{e.Message} 内部异常： {e.InnerException}");
            string result = ApiResponse.Fail("程序运行出错，请稍后重试").ToString();
            await context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder CustomException(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerExtensions>();
        }
    }
}
