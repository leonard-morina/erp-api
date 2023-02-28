using System.Security.Claims;
using Erp.Api.Constants;
using Erp.Core.Entities.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Api;

public class BaseController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;

    public BaseController(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }
    

    public string AuthenticatedUserEmail => GetClaimValueFromContext(ClaimTypeConstants.EMAIL);
    
    protected string AuthenticatedUserId => GetClaimValueFromContext(ClaimTypeConstants.USER_ID);
    protected string CompanyIdByHeaders => GetValueFromHeaders(HeaderConstants.COMPANY_ID);

    private string GetValueFromHeaders(string type)
    {
        HttpContext.Request.Headers.TryGetValue(type, out var value);
        return value;
    }


    [NonAction]
    protected async Task<User> GetAuthenticatedUserAsync()
    {
        if (string.IsNullOrEmpty(AuthenticatedUserId)) return null;
        var user = await _signInManager.UserManager.FindByIdAsync(AuthenticatedUserId);
        return user;
    }

    [NonAction]
    protected BadRequestObjectResult BadRequestWithErrorCode(string errorCode)
    {
        return BadRequest(new {errorCode});
    }

    [NonAction]
    protected string GetClaimValueFromContext(string claimType)
    {
        var claims = (IEnumerable<Claim>) HttpContext.Items["Claims"];
        if (claims == null) return null;
        var claimValue = claims.Where(s => s.Type == claimType).FirstOrDefault().Value;
        return claimValue;
    }
}