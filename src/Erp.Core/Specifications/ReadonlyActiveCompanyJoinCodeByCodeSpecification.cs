using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class ReadonlyActiveCompanyJoinCodeByCodeSpecification : Specification<CompanyJoinCode>
{
    public ReadonlyActiveCompanyJoinCodeByCodeSpecification(string code)
    {
        Query.Where(e => e.JoinCode == code && e.IsActive)
            .AsNoTracking();
    }
}