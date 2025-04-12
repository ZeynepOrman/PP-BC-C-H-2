using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using log4net;

namespace PP_BC_C_H_2.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(LoggingMiddleware));

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log action entry
            _logger.Info($"Action {context.Request.Path} started");

            await _next(context);

            // Log action exit
            _logger.Info($"Action {context.Request.Path} completed");
        }
    }
}
