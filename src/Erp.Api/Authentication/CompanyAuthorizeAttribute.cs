using System.Security.Claims;
using Erp.Api.Constants;
using Erp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Erp.Api.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]

public class CompanyAuthorize : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.Request.Headers.TryGetValue(HeaderConstants.COMPANY_ID, out var companyId);
        if (string.IsNullOrEmpty(companyId))
        {
            context.Result = new JsonResult(new {message = "Company Id was not provided"})
                {StatusCode = StatusCodes.Status404NotFound};
            return;
        }

        var claims = (IEnumerable<Claim>) context.HttpContext.Items["Claims"];
        if (claims == null || claims.Any())
        {
            context.Result = new JsonResult(new {message = "Claims were not found"})
                {StatusCode = StatusCodes.Status401Unauthorized};
            return;
        }

        var companyInClaims =
            claims.FirstOrDefault(e => e.Type == ClaimTypeConstants.COMPANY_ID && e.Value == companyId)?.Value != null;
        if (companyInClaims != null)
        {
            await next();
            return;
        }

        var userId = claims.FirstOrDefault(e => e.Type == ClaimTypeConstants.USER_ID)?.Value;
        if (userId == null)
        {
            context.Result = new JsonResult(new {message = "User Id was not found in claims"})
                {StatusCode = StatusCodes.Status401Unauthorized};
            return;
        }

        var serviceProvider = context.HttpContext.RequestServices;
        var companyService = serviceProvider.GetService<ICompanyService>();
        var userInCompany = await companyService.UserIsInCompanyIdAsync(userId, companyId);
        if (!userInCompany)
        {
            context.Result = new JsonResult(new {message = "User id is not part of the requested company"})
                {StatusCode = StatusCodes.Status401Unauthorized};
            return;
        }

        await next();
    }
}