using Erp.Core.Constants;
using Erp.Core.Error;

namespace Erp.Core.Exceptions;

public class UserJoinRequestsExistsInCompanyException : ExceptionWithErrorCode
{
    public UserJoinRequestsExistsInCompanyException() : base(ErrorCode.USER_JOIN_REQUEST_EXISTS_IN_COMPANY)
    {
        
    }
}