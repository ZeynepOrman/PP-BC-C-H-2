using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PP_BC_C_H_2.Services;

namespace PP_BC_C_H_2.Attributes
{
    public class FakeUserAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var fakeService = context.HttpContext.RequestServices.GetService<IFakeService>();
            var isValidUser = fakeService.ValidateUser("test", "password");

            if (!isValidUser)
            {
                context.Result = new UnauthorizedResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
