using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class ReadonlyCompanyJoinRequestByOwnerIdSpecification : Specification<CompanyJoinRequest>
{
    public ReadonlyCompanyJoinRequestByOwnerIdSpecification(string ownerId)
    {
        Query.Include(s => s.Company)
            .ThenInclude(s => s.UserCompanies)
            .Where(e => e.Company.UserCompanies.Any(e => e.UserId == ownerId && e.IsOwner));
    }
}