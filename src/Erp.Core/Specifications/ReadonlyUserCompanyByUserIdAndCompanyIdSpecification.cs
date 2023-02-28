using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class ReadonlyUserCompanyByUserIdAndCompanyIdSpecification : Specification<UserCompany>
{
    public ReadonlyUserCompanyByUserIdAndCompanyIdSpecification(string userId, string companyId)
    {
        Query.Where(e => e.UserId == userId && e.CompanyId == companyId)
            .AsNoTracking();
    }
}