using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SoftwareMindWeb.Authentication
{
    public class WebAuthenticationHandler : AuthorizationHandler<WebAuthenticationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WebAuthenticationRequirement requirement)
        {
            if (context.Resource is HttpContext authorizationFilterContext &&
                authorizationFilterContext.Request.Cookies.Keys.Any(cook => cook.Contains("AspNetCore")))
            {
                var validationPassed = new Random().Next(1, 999) < 901;
                if (validationPassed)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "AuthenticatedUser")
                    };
                    var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "web-auth"));
                    authorizationFilterContext.User = principal;
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}
