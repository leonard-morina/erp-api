using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Erp.Api.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

public class JwtAuthorize : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        string user = (string)context.HttpContext.Items["User"];
        if (string.IsNullOrEmpty(user))
        {
            context.Result = new JsonResult(new { message = "Unauthorized" })
                { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}