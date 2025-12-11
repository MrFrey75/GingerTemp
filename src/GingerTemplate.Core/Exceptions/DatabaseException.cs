namespace GingerTemplate.Core.Exceptions;

/// <summary>
/// Exception thrown for database operation failures.
/// </summary>
public class DatabaseException : ApplicationException
{
    public DatabaseException(string message) : base(message)
    {
    }

    public DatabaseException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
