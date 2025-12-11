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

/// <summary>
/// Exception thrown for validation failures.
/// </summary>
public class ValidationException : ApplicationException
{
    public ValidationException(string message) : base(message)
    {
    }

    public ValidationException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown for authentication failures.
/// </summary>
public class AuthenticationException : ApplicationException
{
    public AuthenticationException(string message) : base(message)
    {
    }

    public AuthenticationException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}

/// <summary>
/// Exception thrown for authorization failures.
/// </summary>
public class AuthorizationException : ApplicationException
{
    public AuthorizationException(string message) : base(message)
    {
    }

    public AuthorizationException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
