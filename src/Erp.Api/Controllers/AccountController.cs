using Erp.Api.Authentication;
using Erp.Api.Models;
using Erp.Api.ViewModel;
using Erp.Core.Entities.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : BaseController
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    
    private readonly ITokenGenerator _tokenGenerator;

    public AccountController(SignInManager<User> signInManager, UserManager<User> userManager,
        RoleManager<Role> roleManager, ITokenGenerator tokenGenerator) : base(signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _tokenGenerator = tokenGenerator;
    }
    
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] SignIn login, CancellationToken cancellationToken = default)
    {
        var loginResult =
            await _signInManager.PasswordSignInAsync(login.Username, login.Password, false, false);

        if (!loginResult.Succeeded) return Unauthorized(loginResult);
        var user = await _userManager.FindByNameAsync(login.Username);
        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenGenerator.GenerateToken(user.UserName, roles[0]);
        return Ok(new SucceededAuthentication {User = user, Token = token});
    }

    [HttpPost("username/valid")]
    public async Task<IActionResult> IsUsernameValid([FromBody] ValidUsername validUsername,
        CancellationToken cancellationToken = default)
    {
        var validatedUsername = new ValidatedUsername();
        var user = await _userManager.FindByNameAsync(validUsername.Username);
        if (user == null)
        {
            validatedUsername.UsernameExists = false;
        }
        else
        {
            validatedUsername.UsernameExists = true;
            validatedUsername.UsernameIsActive = user.IsActive;
        }

        return Ok(validatedUsername);
    }
    
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = registerUser.Username,
            Email = registerUser.Email,
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            InsertedDateTime = DateTime.Now,
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, registerUser.Password);
        if (!result.Succeeded) return NotFound(result.Errors);

        if (!string.IsNullOrEmpty(registerUser.Role))
        {
            var roleExists = await _roleManager.RoleExistsAsync(registerUser.Role);
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new Role(registerUser.Role));
            }
            await _userManager.AddToRoleAsync(user, registerUser.Role);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _tokenGenerator.GenerateToken(user.UserName, roles[0]);
        return Ok(new SucceededAuthentication {User = user, Token = token});
    }
}