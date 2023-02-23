using Erp.Core.Entities.Account;
using Erp.Core.Interfaces;
using Erp.Core.Specifications;

namespace Erp.Core.Services;

//TODO: add method to search for the companies as well, to remove user from company, to delete from company, etc.
public class CompanyService : ICompanyService
{
    private readonly IAsyncRepository<UserCompany> _userCompanyRepository;
    private readonly IAsyncRepository<Company> _companyRepository;
    private readonly IAsyncRepository<CompanyJoinRequest> _companyJoinRequestRepository;

    public CompanyService(IAsyncRepository<UserCompany> userCompanyRepository,
        IAsyncRepository<Company> companyRepository,
        IAsyncRepository<CompanyJoinRequest> companyJoinRequestRepository)
    {
        _userCompanyRepository = userCompanyRepository;
        _companyRepository = companyRepository;
        _companyJoinRequestRepository = companyJoinRequestRepository;
    }

    public async Task<IReadOnlyList<UserCompany>> GetUserCompaniesByUserIdAsync(string userId,
        CancellationToken cancellationToken = default)
    {
        var userCompaniesSpecification = new ReadonlyUserCompanyByUserIdSpecification(userId);
        return await _userCompanyRepository.ListAsync(userCompaniesSpecification, cancellationToken);
    }

    public async Task<bool> AddCompanyAsync(string companyName, string companyAddress1, string companyAddress2,
        string companyEmail, string companyPhone, string companyWebsite,
        string companyLogo, string companyOwnerFirstName, string companyOwnerLastName,
        string ownerId, bool addOwnerAsPartOfCompany,
        CancellationToken cancellationToken = default)
    {
        var company = new Company
        {
            Name = companyName,
            AddressLine1 = companyAddress1,
            AddressLine2 = companyAddress2,
            Email = companyEmail,
            Logo = companyLogo,
            Phone = companyPhone,
            InsertedByUserId = ownerId,
            OwnerFirstName = companyOwnerFirstName,
            OwnerLastName = companyOwnerLastName,
            Website = companyWebsite,
        };

        var addedCompany = await _companyRepository.AddAsync(company, cancellationToken);
        if (!addedCompany || !addOwnerAsPartOfCompany) return true;
        return await AddUserToCompanyAsync(ownerId, company.CompanyId, ownerId, cancellationToken);
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