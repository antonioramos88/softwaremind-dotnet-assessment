using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SoftwareMindWeb.Authentication
{
    public class WebTokenAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ok = context.HttpContext.Items.TryGetValue("auth-token", out object? token);
            if (ok && Convert.ToInt32(token) < 888)
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "controller", "Home" },
                        { "action", "Error" }
                    });
            }
        }
    }
}
