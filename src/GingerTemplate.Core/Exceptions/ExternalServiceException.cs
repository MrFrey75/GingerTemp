namespace GingerTemplate.Core.Exceptions;

/// <summary>
/// Exception thrown for external service failures.
/// </summary>
public class ExternalServiceException : ApplicationException
{
    public ExternalServiceException(string message) : base(message)
    {
    }

    public ExternalServiceException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
