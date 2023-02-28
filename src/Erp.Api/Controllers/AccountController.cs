using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Erp.Api.Authentication;
using Erp.Api.Constants;
using Erp.Api.Extensions;
using Erp.Api.Models;
using Erp.Api.ViewModel;
using Erp.Core.Entities.Account;
using Erp.Core.Error;
using Erp.Core.Interfaces;
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

    [HttpPost(ApiRoutes.Account.LOGIN)]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] SignIn login, CancellationToken cancellationToken = default)
    {
        var isEmail = new EmailAddressAttribute().IsValid(login.UsernameOrEmail);
        string username;
        User? user = null;
        if (isEmail)
        {
            user = await _userManager.FindByEmailAsync(login.UsernameOrEmail);
            if (user == null)
                return BadRequest(new ValidatedUsernameOrEmail
                {
                    UsernameOrEmailExists = false,
                });
            username = user.UserName;
        }
        else
        {
            username = login.UsernameOrEmail;
        }

        var loginResult =
            await _signInManager.PasswordSignInAsync(username, login.Password, false, false);

        if (!loginResult.Succeeded) return Unauthorized(loginResult);
        user ??= await _userManager.FindByNameAsync(username);
        var roles = await _userManager.GetRolesAsync(user);
        var claims = await _userManager.GetClaimsAsync(user);
        var token = _tokenGenerator.GenerateToken(new TokenGeneratorOptions(user.Id, user.Email, roles[0],
            claims as List<Claim>));
        return Ok(new SucceededAuthentication(user) {Token = token});
    }

    [HttpPost(ApiRoutes.Account.USERNAME_EMAIL_VALID)]
    public async Task<IActionResult> IsUsernameValid([FromBody] ValidUsernameOrEmail validUsernameOrEmail,
        CancellationToken cancellationToken = default)
    {
        var validatedUsernameOrEmail = new ValidatedUsernameOrEmail();
        var isEmail = new EmailAddressAttribute().IsValid(validUsernameOrEmail.Value);
        User? user = null;
        if (isEmail)
        {
            user = await _userManager.FindByEmailAsync(validUsernameOrEmail.Value);
        }
        else
        {
            user = await _userManager.FindByNameAsync(validUsernameOrEmail.Value);
        }

        if (user == null)
        {
            validatedUsernameOrEmail.UsernameOrEmailExists = false;
        }
        else
        {
            validatedUsernameOrEmail.UsernameOrEmailExists = true;
            validatedUsernameOrEmail.UsernameOrEmailIsActive = user.IsActive;
        }

        return Ok(validatedUsernameOrEmail);
    }

    [HttpPost(ApiRoutes.Account.REGISTER)]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUser registerUser)
    {
        try
        {
            await _userManager.ValidateIfUsernameOrEmailExistsAsync(registerUser.Email, registerUser.Username);
        }
        catch (ExceptionWithErrorCode ex)
        {
            return BadRequestWithErrorCode(ex.Message);
        }

        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            UserName = registerUser.Username,
            Email = registerUser.Email,
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            InsertedDateTime = DateTime.Now,
            EmailConfirmed = true,
            IsActive = true
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
        var token = _tokenGenerator.GenerateToken(new TokenGeneratorOptions(user.Id, user.Email, roles[0]));
        return Ok(new SucceededAuthentication(user) {Token = token});
    }
}