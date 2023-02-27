using Erp.Core.Constants;
using Erp.Core.Error;

namespace Erp.Core.Exceptions;

public class EmailExistsException : ExceptionWithErrorCode
{
    public EmailExistsException() : base(ErrorCode.EMAIL_EXISTS)
    {
        
    }
}