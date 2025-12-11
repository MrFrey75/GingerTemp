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

/// <summary>
/// Exception thrown for configuration errors.
/// </summary>
public class ConfigurationException : ApplicationException
{
    public ConfigurationException(string message) : base(message)
    {
    }

    public ConfigurationException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
