using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

/// <summary>
/// Interface for logging service operations.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
/// services.AddSingleton&lt;ILoggingService, LoggingService&gt;();
/// var log = provider.GetRequiredService&lt;ILoggingService&gt;();
/// using var scope = log.BeginCorrelationScope();
/// log.LogInformation("Processing request", new Dictionary&lt;string, object?&gt; { { "UserId", 42 } });
/// </code>
/// </remarks>
public interface ILoggingService
{
    string CurrentCorrelationId { get; }

    IDisposable BeginScope<TState>(TState state) where TState : notnull;
    IDisposable BeginCorrelationScope(string? correlationId = null);

    void LogInformation(string message, IDictionary<string, object?>? properties = null);
    void LogWarning(string message, IDictionary<string, object?>? properties = null);
    void LogError(string message, Exception? exception = null, IDictionary<string, object?>? properties = null);
    void LogDebug(string message, IDictionary<string, object?>? properties = null);
}

/// <summary>
/// Implementation of logging service using structured logging.
/// </summary>
public class LoggingService : ILoggingService
{
    private readonly ILogger<LoggingService> _logger;
    private static readonly AsyncLocal<string?> CorrelationContext = new();

    public LoggingService(ILogger<LoggingService> logger)
    {
        _logger = logger;
        _logger.LogInformation("LoggingService initialized.");
    }

    public string CurrentCorrelationId => EnsureCorrelationId();

    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        ArgumentNullException.ThrowIfNull(state);
        return _logger.BeginScope(state) ?? NoopDisposable.Instance;
    }

    public IDisposable BeginCorrelationScope(string? correlationId = null)
    {
        var id = EnsureCorrelationId(correlationId);
        var scope = new Dictionary<string, object?> { ["CorrelationId"] = id };
        return _logger.BeginScope(scope) ?? NoopDisposable.Instance;
    }

    public void LogInformation(string message, IDictionary<string, object?>? properties = null)
    {
        LogWithScope(() => _logger.LogInformation(message), properties);
    }

    public void LogWarning(string message, IDictionary<string, object?>? properties = null)
    {
        LogWithScope(() => _logger.LogWarning(message), properties);
    }

    public void LogError(string message, Exception? exception = null, IDictionary<string, object?>? properties = null)
    {
        LogWithScope(() => _logger.LogError(exception, message), properties);
    }

    public void LogDebug(string message, IDictionary<string, object?>? properties = null)
    {
        LogWithScope(() => _logger.LogDebug(message), properties);
    }

    private void LogWithScope(Action logAction, IDictionary<string, object?>? properties)
    {
        var merged = MergeProperties(properties);
        using var scope = _logger.BeginScope(merged);
        logAction();
    }

    private IDictionary<string, object?> MergeProperties(IDictionary<string, object?>? properties)
    {
        var merged = properties != null
            ? new Dictionary<string, object?>(properties)
            : new Dictionary<string, object?>();

        var correlationId = EnsureCorrelationId();
        merged["CorrelationId"] = correlationId;
        return merged;
    }

    private string EnsureCorrelationId(string? correlationId = null)
    {
        if (!string.IsNullOrWhiteSpace(correlationId))
        {
            CorrelationContext.Value = correlationId;
            return correlationId;
        }

        if (!string.IsNullOrWhiteSpace(CorrelationContext.Value))
        {
            return CorrelationContext.Value!;
        }

        var resolved = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();
        CorrelationContext.Value = resolved;
        return resolved;
    }

    private sealed class NoopDisposable : IDisposable
    {
        public static readonly IDisposable Instance = new NoopDisposable();
        public void Dispose() { }
    }
}
