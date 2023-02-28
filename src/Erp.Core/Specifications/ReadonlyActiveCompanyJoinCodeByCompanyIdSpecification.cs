using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class ReadonlyActiveCompanyJoinCodeByCompanyIdSpecification : Specification<CompanyJoinCode>
{
    public ReadonlyActiveCompanyJoinCodeByCompanyIdSpecification(string companyId)
    {
        Query.Where(e => e.CompanyId == companyId && e.IsActive)
            .AsNoTracking();
    }
}