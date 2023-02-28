using Erp.Core.Entities.Account;
using Erp.Core.Exceptions;
using Erp.Core.Interfaces;
using Erp.Core.Specifications;

namespace Erp.Core.Services;

//TODO: add method to search for the companies as well, to remove user from company, to delete from company, etc.
public class CompanyService : ICompanyService
{
    private readonly IAsyncRepository<UserCompany> _userCompanyRepository;
    private readonly IAsyncRepository<Company> _companyRepository;
    private readonly IAsyncRepository<CompanyJoinRequest> _companyJoinRequestRepository;
    private readonly IAsyncRepository<CompanyJoinCode> _companyJoinCodeRepository;

    public CompanyService(IAsyncRepository<UserCompany> userCompanyRepository,
        IAsyncRepository<Company> companyRepository,
        IAsyncRepository<CompanyJoinRequest> companyJoinRequestRepository,
        IAsyncRepository<CompanyJoinCode> companyJoinCodeRepository)
    {
        _userCompanyRepository = userCompanyRepository;
        _companyRepository = companyRepository;
        _companyJoinRequestRepository = companyJoinRequestRepository;
        _companyJoinCodeRepository = companyJoinCodeRepository;
    }

    public async Task<IReadOnlyList<UserCompany>> GetUserCompaniesByUserIdAsync(string userId,
        CancellationToken cancellationToken = default)
    {
        var userCompaniesSpecification = new ReadonlyUserCompanyByUserIdSpecification(userId);
        return await _userCompanyRepository.ListAsync(userCompaniesSpecification, cancellationToken);
    }

    public async Task<string> AddCompanyAsync(string companyName, string companyAddress1, string companyAddress2,
        string companyEmail, string companyPhone, string companyWebsite,
        string companyLogo, string companyOwnerFirstName, string companyOwnerLastName,
        string country, string city, string ownerId, bool addOwnerAsPartOfCompany,
        CancellationToken cancellationToken = default)
    {
        var company = new Company(ownerId)
        {
            Name = companyName,
            AddressLine1 = companyAddress1,
            AddressLine2 = companyAddress2,
            Email = companyEmail,
            Logo = companyLogo,
            Phone = companyPhone,
            OwnerFirstName = companyOwnerFirstName,
            OwnerLastName = companyOwnerLastName,
            Website = companyWebsite,
            City = city,
            Country = country
        };

        var addedCompany = await _companyRepository.AddAsync(company, cancellationToken);
        if (!addedCompany || !addOwnerAsPartOfCompany) return company.CompanyId;
        var registered = await AddUserToCompanyAsync(ownerId, company.CompanyId, ownerId, cancellationToken);
        return registered ? company.CompanyId : null;
    }

    private async Task<bool> AddUserToCompanyAsync(string userId, string companyId,
        string insertedByUserId,
        CancellationToken cancellationToken = default)
    {
        var userCompany = new UserCompany
        {
            CompanyId = companyId,
            IsOwner = true,
            UserId = userId,
            InsertedByUserId = insertedByUserId,
        };

        return await _userCompanyRepository.AddAsync(userCompany, cancellationToken);
    }

    public async Task<string> GetActiveCompanyCodeByCompanyIdAsync(string companyId,
        CancellationToken cancellationToken = default)
    {
        var companyJoinCodeSpecification = new ReadonlyActiveCompanyJoinCodeByCompanyIdSpecification(companyId);
        var companyJoinCode =
            await _companyJoinCodeRepository.FirstOrDefaultAsync(companyJoinCodeSpecification, cancellationToken);
        if (companyJoinCode == null) return null;
        return companyJoinCode.JoinCode;
    }

    public async Task<string> GetCompanyIdByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        var companyJoinCodeSpecification = new ReadonlyActiveCompanyJoinCodeByCodeSpecification(code);
        var companyJoinCode =
            await _companyJoinCodeRepository.FirstOrDefaultAsync(companyJoinCodeSpecification, cancellationToken);
        if (companyJoinCode == null) throw new InvalidCompanyJoinCodeException();
        return companyJoinCode.CompanyId;
    }

    public async Task<bool> UserIsInCompanyIdAsync(string userId, string companyId,
        CancellationToken cancellationToken = default)
    {
        var userCompanySpecification = new ReadonlyUserCompanyByUserIdAndCompanyIdSpecification(userId, companyId);
        return await _userCompanyRepository.FirstOrDefaultAsync(userCompanySpecification, cancellationToken) != null;
    }

    public async Task<bool> RequestToJoinCompanyAsync(string userId, string companyId,
        bool requestInitiatedByCompany,
        CancellationToken cancellationToken = default)
    {
        var companyJoinRequestSpecification =
            new ReadonlyCompanyJoinRequestByUserIdAndCompanyIdSpecification(userId, companyId);
        var companyJoinRequest =
            await _companyJoinRequestRepository.FirstOrDefaultAsync(companyJoinRequestSpecification, cancellationToken);
        //TODO: Add exception
        if (companyJoinRequest != null) return false;

        return await _companyJoinRequestRepository.AddAsync(new CompanyJoinRequest
        {
            UserId = userId,
            CompanyId = companyId,
            InsertedByUserId = userId,
            RequestInitiatedByCompany = requestInitiatedByCompany
        }, cancellationToken);
    }

    public async Task<bool> ApproveCompanyJoinRequest(string companyId, string userId, string userIdToJoin,
        CancellationToken cancellationToken = default)
    {
        var companyJoinRequestSpecification =
            new ActiveCompanyJoinRequestByUserIdAndCompanyIdSpecification(userIdToJoin, companyId);
        var companyJoinRequest =
            await _companyJoinRequestRepository.FirstOrDefaultAsync(companyJoinRequestSpecification, cancellationToken);
        //TODO: Add exception
        if (companyJoinRequest == null) return false;
        companyJoinRequest.ApproveRequest(userId);
        var updatedCompanyJoinRequest =
            await _companyJoinRequestRepository.UpdateAsync(companyJoinRequest, cancellationToken);
        if (updatedCompanyJoinRequest)
        {
            return await AddUserToCompanyAsync(userIdToJoin, companyId, userId, cancellationToken);
        }

        return false;
    }
}