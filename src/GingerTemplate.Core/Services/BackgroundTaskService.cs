using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

/// <summary>
/// Contract for managing background task scopes to ensure start/end symmetry.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
/// services.AddSingleton&lt;IBackgroundTaskService, BackgroundTaskService&gt;();
/// var tasks = provider.GetRequiredService&lt;IBackgroundTaskService&gt;();
/// using var scope = tasks.StartBackgroundTask("ImportJob");
/// // work here, Dispose ends the task automatically
/// </code>
/// </remarks>
public interface IBackgroundTaskService
{
    IDisposable StartBackgroundTask(string taskName);
    void EndBackgroundTask(string taskName);
}

public class BackgroundTaskService : IBackgroundTaskService
{
    private readonly ILogger<BackgroundTaskService> _logger;
    private static readonly AsyncLocal<Stack<string>> TaskStackContext = new();

    public BackgroundTaskService(ILogger<BackgroundTaskService> logger)
    {
        _logger = logger;
        _logger.LogInformation("BackgroundTaskService initialized.");
    }

    public IDisposable StartBackgroundTask(string taskName)
    {
        ArgumentNullException.ThrowIfNull(taskName);

        var stack = TaskStackContext.Value ??= new Stack<string>();
        stack.Push(taskName);
        _logger.LogInformation("Started background task: {TaskName}", taskName);

        return new BackgroundTaskScope(this, taskName);
    }

    public void EndBackgroundTask(string taskName)
    {
        var stack = TaskStackContext.Value;
        if (stack == null || stack.Count == 0 || stack.Peek() != taskName)
        {
            throw new InvalidOperationException("Mismatched background task end call.");
        }

        stack.Pop();
        _logger.LogInformation("Ended background task: {TaskName}", taskName);
    }

    private class BackgroundTaskScope : IDisposable
    {
        private readonly BackgroundTaskService _service;
        private readonly string _taskName;
        private bool _disposed;

        public BackgroundTaskScope(BackgroundTaskService service, string taskName)
        {
            _service = service;
            _taskName = taskName;
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _service.EndBackgroundTask(_taskName);
                _disposed = true;
            }
        }
    }
}