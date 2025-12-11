namespace GingerTemplate.Core.Exceptions;

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
