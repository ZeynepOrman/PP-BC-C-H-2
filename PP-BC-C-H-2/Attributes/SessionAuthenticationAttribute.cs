using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PP_BC_C_H_2.Extensions;

namespace PP_BC_C_H_2.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SessionAuthenticationAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var session = context.HttpContext.Session;
            if (!session.IsAuthenticated())
            {
                context.Result = new UnauthorizedObjectResult(new { status = 401, error = "Unauthorized" });
            }
        }
    }
}
