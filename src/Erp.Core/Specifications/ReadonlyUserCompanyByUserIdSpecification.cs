using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class ReadonlyUserCompanyByUserIdSpecification : Specification<UserCompany>
{
    public ReadonlyUserCompanyByUserIdSpecification(string userId)
    {
        Query.Where(e => e.UserId == userId)
            .Include(s => s.Company)
            .AsNoTracking();
    }
}