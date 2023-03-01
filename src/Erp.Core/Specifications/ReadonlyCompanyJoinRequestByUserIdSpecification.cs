using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class ReadonlyCompanyJoinRequestByUserIdSpecification : Specification<CompanyJoinRequest>
{
    public ReadonlyCompanyJoinRequestByUserIdSpecification(string userId)
    {
        Query.Where(e => e.UserId == userId)
            .AsNoTracking();
    }
}