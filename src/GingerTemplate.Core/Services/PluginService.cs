using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

public interface IPluginService
{
    void LoadPlugin(string pluginPath);
    IEnumerable<string> GetLoadedPlugins();
}

public class PluginService : IPluginService
{
    private readonly ILogger<PluginService> _logger;
    private readonly List<string> _loadedPlugins = new();
    private readonly ReaderWriterLockSlim _lock = new();

    public PluginService(ILogger<PluginService> logger)
    {
        _logger = logger;
    }

    public void LoadPlugin(string pluginPath)
    {
        _lock.EnterWriteLock();
        try
        {
            // Simulate plugin loading
            _loadedPlugins.Add(pluginPath);
            _logger.LogInformation("Plugin loaded from {PluginPath}.", pluginPath);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public IEnumerable<string> GetLoadedPlugins()
    {
        _lock.EnterReadLock();
        try
        {
            _logger.LogInformation("Retrieving list of loaded plugins.");
            return new List<string>(_loadedPlugins);
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
}