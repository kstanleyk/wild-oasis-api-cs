using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WildOasis.API.Core
{
    public class JwtAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the user is authenticated using JwtBearer authentication scheme
            if (!context.HttpContext.User.Identity!.IsAuthenticated ||
                !context.HttpContext.User.Identity.AuthenticationType!.Equals(JwtBearerDefaults.AuthenticationScheme))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
            }
        }
    }
}
