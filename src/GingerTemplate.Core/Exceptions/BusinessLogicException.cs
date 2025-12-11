namespace GingerTemplate.Core.Exceptions;

/// <summary>
/// Exception thrown for business logic violations.
/// </summary>
public class BusinessLogicException : ApplicationException
{
    public BusinessLogicException(string message) : base(message)
    {
    }

    public BusinessLogicException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
