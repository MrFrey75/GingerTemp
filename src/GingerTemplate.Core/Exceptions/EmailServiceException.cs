namespace GingerTemplate.Core.Exceptions;

/// <summary>
/// Exception thrown for email service failures.
/// </summary>
public class EmailServiceException : ExternalServiceException
{
    public EmailServiceException(string message) : base(message)
    {
    }

    public EmailServiceException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
