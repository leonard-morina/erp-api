using Erp.Core.Constants;
using Erp.Core.Error;

namespace Erp.Core.Exceptions;

public class InvalidCompanyJoinCodeException : ExceptionWithErrorCode
{
    public InvalidCompanyJoinCodeException() : base(ErrorCode.INVALID_COMPANY_JOIN_CODE)
    {
        
    }
}