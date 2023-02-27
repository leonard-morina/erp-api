using Erp.Core.Constants;
using Erp.Core.Error;

namespace Erp.Core.Exceptions;

public class UsernameExistsException : ExceptionWithErrorCode
{
    public UsernameExistsException() : base(ErrorCode.USERNAME_EXISTS)
    {
        
    }
}