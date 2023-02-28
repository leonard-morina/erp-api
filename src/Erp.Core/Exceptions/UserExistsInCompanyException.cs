using Erp.Core.Constants;
using Erp.Core.Error;

namespace Erp.Core.Exceptions;

public class UserExistsInCompanyException : ExceptionWithErrorCode
{
    public UserExistsInCompanyException() : base(ErrorCode.USER_EXISTS_IN_COMPANY)
    {
        
    }
}