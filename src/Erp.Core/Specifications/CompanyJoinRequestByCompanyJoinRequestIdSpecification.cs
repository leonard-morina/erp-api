using Ardalis.Specification;
using Erp.Core.Entities.Account;

namespace Erp.Core.Specifications;

public class CompanyJoinRequestByCompanyJoinRequestIdSpecification : Specification<CompanyJoinRequest>
{
    public CompanyJoinRequestByCompanyJoinRequestIdSpecification(string companyJoinRequestId)
    {
        Query.Where(e => e.CompanyJoinRequestId == companyJoinRequestId);
    }
}