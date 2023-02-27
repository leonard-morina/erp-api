namespace Erp.Core.Error;

public class ExceptionWithErrorCode : Exception
{
    protected ExceptionWithErrorCode(string errorCode) : base(errorCode)
    {

    }
}