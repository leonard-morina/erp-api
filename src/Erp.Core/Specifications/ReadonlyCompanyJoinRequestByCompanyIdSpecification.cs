using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class ReadonlyCompanyJoinRequestByCompanyIdSpecification : Specification<CompanyJoinRequest>
{
    public ReadonlyCompanyJoinRequestByCompanyIdSpecification(string companyId)
    {
        Query.Where(e => e.CompanyId == companyId)
            .AsNoTracking();
    }
}