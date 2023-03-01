using Erp.Core.Constants;
using Erp.Core.Error;

namespace Erp.Core.Exceptions;

public class ReviewHasAlreadyBeenDoneForCompanyJoinRequestException : ExceptionWithErrorCode
{
    public ReviewHasAlreadyBeenDoneForCompanyJoinRequestException() : base(ErrorCode.REVIEW_HAS_ALREADY_BEEN_DONE_FOR_COMPANY_JOIN_REQUEST)
    {
    }
}