using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PP_BC_C_H_2.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Log action entry
            Console.WriteLine($"Action {context.Request.Path} started");

            await _next(context);

            // Log action exit
            Console.WriteLine($"Action {context.Request.Path} completed");
        }
    }
}
