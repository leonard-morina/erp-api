using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class ReadonlyCompanyJoinRequestByUserIdAndCompanyIdSpecification : Specification<CompanyJoinRequest>
{
    public ReadonlyCompanyJoinRequestByUserIdAndCompanyIdSpecification(string userId, string companyId)
    {
        Query.Where(e => e.UserId == userId && e.CompanyId == companyId).AsNoTracking();
    }
}