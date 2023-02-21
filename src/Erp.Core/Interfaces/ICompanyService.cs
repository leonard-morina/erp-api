using Erp.Core.Entities.Account;

namespace Erp.Core.Interfaces;

public interface ICompanyService
{
    Task<IReadOnlyList<UserCompany>> GetUserCompaniesByUserIdAsync(string userId,
        CancellationToken cancellationToken = default);
    Task<bool> AddCompanyAsync(string companyName, string companyAddress, string companyEmail,
        string companyPhone, string companyWebsite,
        string companyLogo, string companyOwnerFirstName, string companyOwnerLastName,
        string ownerId, bool addOwnerAsPartOfCompany,
        CancellationToken cancellationToken = default);
    Task<bool> RequestToJoinCompanyAsync(string userId, string companyId,
        bool requestInitiatedByCompany,
        CancellationToken cancellationToken = default);
    Task<bool> ApproveCompanyJoinRequest(string companyId, string userId, string userIdToJoin,
        CancellationToken cancellationToken = default);
}