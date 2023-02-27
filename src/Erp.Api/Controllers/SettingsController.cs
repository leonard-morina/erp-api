using Erp.Core.Entities.Account;
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
}