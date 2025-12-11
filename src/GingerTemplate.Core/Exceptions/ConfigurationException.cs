namespace GingerTemplate.Core.Exceptions;

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
