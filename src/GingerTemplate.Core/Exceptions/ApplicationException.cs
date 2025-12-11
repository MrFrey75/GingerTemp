namespace GingerTemplate.Core.Exceptions;

/// <summary>
/// Base exception for all application exceptions.
/// </summary>
public class ApplicationException : Exception
{
    public ApplicationException(string message) : base(message)
    {
    }

    public ApplicationException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
