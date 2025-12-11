namespace GingerTemplate.Core.Configuration;

/// <summary>
/// Application settings configuration.
/// </summary>
public class ApplicationSettings
{
    public string AppName { get; set; } = "GingerTemplate";
    public string AppVersion { get; set; } = "1.0.0";
    public string Environment { get; set; } = "Development";
    public bool EnableDetailedErrors { get; set; } = true;
}

/// <summary>
/// Database configuration settings.
/// </summary>
public class DatabaseSettings
{
    public string ConnectionString { get; set; } = "Data Source=gingerdb.db";
    public bool EnableMigrations { get; set; } = true;
    public int CommandTimeout { get; set; } = 30;
}

/// <summary>
/// Logging configuration settings.
/// </summary>
public class LoggingSettings
{
    public string LogLevel { get; set; } = "Information";
    public string[] Sinks { get; set; } = { "Console", "File" };
    public string LogFormat { get; set; } = "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}";
    public bool EnableRequestLogging { get; set; } = true;
}

/// <summary>
/// API configuration settings.
/// </summary>
public class ApiSettings
{
    public string ApiName { get; set; } = "GingerTemplate API";
    public string ApiVersion { get; set; } = "v1";
    public bool EnableSwagger { get; set; } = true;
    public bool EnableCors { get; set; } = true;
    public string[] AllowedOrigins { get; set; } = { "http://localhost:3000", "http://localhost:5173" };
}

/// <summary>
/// Security configuration settings.
/// </summary>
public class SecuritySettings
{
    public string JwtSecret { get; set; } = "change-me-in-production-with-strong-secret-key";
    public int JwtExpirationMinutes { get; set; } = 60;
    public bool RequireHttps { get; set; } = false;
    public int PasswordMinLength { get; set; } = 8;
    public bool EnableTwoFactor { get; set; } = false;
}

/// <summary>
/// Email configuration settings.
/// </summary>
public class EmailSettings
{
    public string SmtpServer { get; set; } = "localhost";
    public int SmtpPort { get; set; } = 25;
    public string FromAddress { get; set; } = "noreply@gingertemplate.local";
    public string FromName { get; set; } = "GingerTemplate";
    public bool EnableSsl { get; set; } = false;
}

/// <summary>
/// Cache configuration settings.
/// </summary>
public class CacheSettings
{
    public bool EnableCache { get; set; } = true;
    public string CacheType { get; set; } = "Memory"; // Memory, Redis
    public int DefaultExpiration { get; set; } = 300;
}

/// <summary>
/// Root configuration object combining all settings.
/// </summary>
public class ApplicationConfiguration
{
    public ApplicationSettings Application { get; set; } = new();
    public DatabaseSettings Database { get; set; } = new();
    public LoggingSettings Logging { get; set; } = new();
    public ApiSettings Api { get; set; } = new();
    public SecuritySettings Security { get; set; } = new();
    public EmailSettings Email { get; set; } = new();
    public CacheSettings Cache { get; set; } = new();
}
