namespace GingerTemplate.Core.Exceptions;

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
