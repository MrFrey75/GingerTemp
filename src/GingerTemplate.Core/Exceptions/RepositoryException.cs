namespace GingerTemplate.Core.Exceptions;

/// <summary>
/// Exception thrown for repository/data access failures.
/// </summary>
public class RepositoryException : ApplicationException
{
    public RepositoryException(string message) : base(message)
    {
    }

    public RepositoryException(string message, Exception innerException) 
        : base(message, innerException)
    {
    }
}
