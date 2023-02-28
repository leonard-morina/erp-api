using Erp.Core.Entities.Account;

namespace Erp.Core.Interfaces;

public interface ICompanyService
{
    Task<IReadOnlyList<UserCompany>> GetUserCompaniesByUserIdAsync(string userId,
        CancellationToken cancellationToken = default);
    Task<string> AddCompanyAsync(string companyName, string companyAddress1, string companyAddress2, 
        string companyEmail, string companyPhone, string companyWebsite,
        string companyLogo, string companyOwnerFirstName, string companyOwnerLastName,
        string country, string city, 
        string ownerId, bool addOwnerAsPartOfCompany,
        CancellationToken cancellationToken = default);
    Task<bool> RequestToJoinCompanyAsync(string userId, string companyId,
        bool requestInitiatedByCompany,
        CancellationToken cancellationToken = default);
    Task<bool> ApproveCompanyJoinRequest(string companyId, string userId, string userIdToJoin,
        CancellationToken cancellationToken = default);
    Task<string> GetCompanyIdByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<string> GetActiveCompanyCodeByCompanyIdAsync(string companyId,
        CancellationToken cancellationToken = default);
    Task<bool> UserIsInCompanyIdAsync(string userId, string companyId,
        CancellationToken cancellationToken = default);
}