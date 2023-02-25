using Erp.Api.Constants;
using Erp.Core.Entities.Account;
using Erp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class SettingsController : BaseController
{
    public SettingsController(SignInManager<User> signInManager) : base(signInManager)
    {
    }

    [HttpGet(ApiRoutes.Settings.LOCALES)]
    public async Task<IActionResult> GetLanguageLocales()
    {
        return Ok(await LocaleDataFetcher.GetLocalesJsonContentAsStringAsync());
    }
}