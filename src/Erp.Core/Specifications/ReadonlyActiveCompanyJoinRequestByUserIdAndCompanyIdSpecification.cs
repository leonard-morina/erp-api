using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class ActiveCompanyJoinRequestByUserIdAndCompanyIdSpecification: Specification<CompanyJoinRequest>
{
    public ActiveCompanyJoinRequestByUserIdAndCompanyIdSpecification(string userId, string companyId)
    {
        Query.Where(e => e.UserId == userId && e.CompanyId == companyId && (!e.RequestApproved && !e.RequestCancelled))
            .AsNoTracking();
    }
}