using Erp.Api.Authentication;
using Erp.Api.Configuration;
using Erp.Api.Files;
using Erp.Api.Models;
using Erp.Api.ViewModel;
using Erp.Core.Entities.Account;
using Erp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : BaseController
{
    private readonly ICompanyService _companyService;
    private readonly IFileUploader _fileUploader;
    private readonly FoldersConfiguration _foldersConfiguration;

    public CompanyController(SignInManager<User> signInManager, ICompanyService companyService,
        IFileUploader fileUploader, FoldersConfiguration foldersConfiguration) : base(signInManager)
    {
        _companyService = companyService;
        _fileUploader = fileUploader;
        _foldersConfiguration = foldersConfiguration;
    }

    [HttpGet("list/my")]
    [JwtAuthorize]
    public async Task<IActionResult> GetAuthenticatedUsersCompany(CancellationToken cancellationToken = default)
    {
        var user = await GetAuthenticatedUserAsync();
        if (user == null) return StatusCode(401);
        var companies = await _companyService.GetUserCompaniesByUserIdAsync(user.Id, cancellationToken);
        var companyWithLogoUrl = companies.Select(userCompany => new CompanyWithLogoUrl
        {
            CompanyName = userCompany.Company.Name,
            CompanyOwnerFirstName = userCompany.Company.OwnerFirstName,
            CompanyOwnerLastName = userCompany.Company.OwnerLastName,
            CompanyId = userCompany.CompanyId,
            IsOwner = userCompany.IsOwner,
            CompanyLogoUrl = _fileUploader.GetFileUrl(userCompany.Company.Logo, _foldersConfiguration.CompanyLogo)
        }).ToList();
        return Ok(companyWithLogoUrl);
    }

    [HttpPost("create")]
    [JwtAuthorize]
    public async Task<IActionResult> CreateCompany([FromForm] RegisterCompany companyRegister,
        CancellationToken cancellationToken = default)
    {
        var user = await GetAuthenticatedUserAsync();
        if (user == null) return StatusCode(401);
        string logoFileName = null;
        if (companyRegister.Logo != null)
        {
            try
            {
                logoFileName = await _fileUploader.UploadFileAsync(companyRegister.Logo, _foldersConfiguration.CompanyLogo,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }    
        }

        var registeredCompany = await _companyService.AddCompanyAsync(companyRegister.Name,
            companyRegister.AddressLine1, companyRegister.AddressLine2,
            companyRegister.Email, companyRegister.PhoneNumber, companyRegister.Website, logoFileName,
            companyRegister.OwnerFirstName, companyRegister.OwnerLastName,
            companyRegister.Country, companyRegister.City,
            user.Id, true, cancellationToken);

        if (!registeredCompany) return BadRequest("Failed to register company");
        return Ok();
    }
}