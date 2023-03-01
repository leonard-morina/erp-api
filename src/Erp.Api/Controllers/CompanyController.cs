using System.Security.Claims;
using Erp.Api.Authentication;
using Erp.Api.Cache;
using Erp.Api.Configuration;
using Erp.Api.Constants;
using Erp.Api.Files;
using Erp.Api.Models;
using Erp.Api.ViewModel;
using Erp.Core.Entities.Account;
using Erp.Core.Error;
using Erp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Erp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly ICompanyService _companyService;
    private readonly IFileUploader _fileUploader;
    private readonly FoldersConfiguration _foldersConfiguration;

    public CompanyController(SignInManager<User> signInManager, UserManager<User> userManager,
        ICompanyService companyService,
        IFileUploader fileUploader, FoldersConfiguration foldersConfiguration) : base(signInManager)
    {
        _userManager = userManager;
        _companyService = companyService;
        _fileUploader = fileUploader;
        _foldersConfiguration = foldersConfiguration;
    }

    [HttpGet(ApiRoutes.Company.MY_LIST)]
    [JwtAuthorize]
    [Cached(TimeConstants.WEEK_IN_SECONDS, true)]
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

    [HttpPost(ApiRoutes.Company.CREATE)]
    [JwtAuthorize]
    [RemoveCacheKeysOnSuccess(ApiRoutes.Company.MY_LIST)]
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
                logoFileName = await _fileUploader.UploadFileAsync(companyRegister.Logo,
                    _foldersConfiguration.CompanyLogo,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        var registeredCompanyId = await _companyService.AddCompanyAsync(companyRegister.Name,
            companyRegister.AddressLine1, companyRegister.AddressLine2,
            companyRegister.Email, companyRegister.PhoneNumber, companyRegister.Website, logoFileName,
            companyRegister.OwnerFirstName, companyRegister.OwnerLastName,
            companyRegister.Country, companyRegister.City,
            user.Id, true, cancellationToken);

        if (string.IsNullOrEmpty(registeredCompanyId)) return BadRequest("Failed to register company");

        await _userManager.AddClaimAsync(user, new Claim(ClaimTypeConstants.COMPANY_ID, registeredCompanyId));
        return Ok();
    }

    [HttpPost(ApiRoutes.Company.REQUEST_JOIN)]
    [JwtAuthorize]
    public async Task<IActionResult> RequestToJoinCompany([FromBody] RequestToJoinCompany requestToJoinCompany,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(AuthenticatedUserId)) return StatusCode(401);
        try
        {
            var companyId = await _companyService.GetCompanyIdByCodeAsync(requestToJoinCompany.Code, cancellationToken);
            var requested =
                await _companyService.RequestToJoinCompanyAsync(AuthenticatedUserId, companyId, false,
                    cancellationToken);
            if (requested)
            {
                return Ok();
            }

            return BadRequest();
        }
        catch (ExceptionWithErrorCode ex)
        {
            return BadRequestWithErrorCode(ex.Message);
        }
    }

    [HttpGet(ApiRoutes.Company.GET_JOIN_REQUESTS_AS_OWNER)]
    [JwtAuthorize]
    public async Task<IActionResult> GetRequestsByCompany(CancellationToken cancellationToken = default)
    {
        var companyJoinRequests =
            await _companyService.GetJoinRequestsByOwnerId(AuthenticatedUserId, cancellationToken);
        var detailedCompanyJoinRequests = companyJoinRequests.Select(companyJoinRequest =>
            new CompanyJoinRequestDetailed
            {
                CompanyName = companyJoinRequest.Company.Name,
                CompanyOwnerFirstName = companyJoinRequest.Company.OwnerFirstName,
                CompanyOwnerLastName = companyJoinRequest.Company.OwnerLastName,
                UserId = companyJoinRequest.UserId,
                CompanyId = companyJoinRequest.CompanyId,
                CompanyLogoUrl =
                    _fileUploader.GetFileUrl(companyJoinRequest.Company.Logo, _foldersConfiguration.CompanyLogo),
                UserFirstName = companyJoinRequest.User.FirstName,
                UserLastName = companyJoinRequest.User.LastName,
                UserEmail = companyJoinRequest.User.Email,
            });
        return Ok(detailedCompanyJoinRequests);
    }

    [HttpGet(ApiRoutes.Company.JOIN_CODE_BY_COMPANY_ID)]
    [JwtAuthorize]
    [CompanyAuthorize]
    public async Task<IActionResult> GetCompanyCode(CancellationToken cancellationToken = default)
    {
        var companyCode =
            await _companyService.GetActiveCompanyCodeByCompanyIdAsync(CompanyIdByHeaders, cancellationToken);
        if (string.IsNullOrEmpty(companyCode)) return NotFound("Company code was not found");
        return Ok(companyCode);
    }
}