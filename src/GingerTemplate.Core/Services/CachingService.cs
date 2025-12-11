using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

/// <summary>
/// Provides lightweight in-memory caching with optional expiration and simple get/set semantics.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
/// services.AddSingleton&lt;ICachingService, CachingService&gt;();
/// var cache = provider.GetRequiredService&lt;ICachingService&gt;();
/// cache.SetItem("token", myToken, TimeSpan.FromMinutes(30));
/// var token = cache.GetItem&lt;string&gt;("token");
/// </code>
/// </remarks>
public interface ICachingService
{
    TItem? GetItem<TItem>(string key);
    void SetItem<TItem>(string key, TItem item, TimeSpan? expiration = null);
    void RemoveItem(string key);
    bool ContainsItem(string key);
}

public class CachingService : ICachingService
{
    private readonly ILogger<CachingService> _logger;
    private readonly Dictionary<string, (object Item, DateTime? Expiration)> _cache = new();
    private readonly ReaderWriterLockSlim _lock = new();

    public CachingService(ILogger<CachingService> logger)
    {
        _logger = logger;
        _logger.LogInformation("CachingService initialized.");
    }

    public TItem? GetItem<TItem>(string key)
    {
        _lock.EnterReadLock();
        try
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                if (entry.Expiration == null || entry.Expiration > DateTime.UtcNow)
                {
                    return (TItem)entry.Item;
                }
                else
                {
                    _logger.LogInformation("Cache item with key {Key} has expired.", key);
                    RemoveItem(key);
                }
            }
            return default;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
    public void SetItem<TItem>(string key, TItem item, TimeSpan? expiration = null)
    {
        var expirationTime = expiration.HasValue ? DateTime.UtcNow.Add(expiration.Value) : (DateTime?)null;

        _lock.EnterWriteLock();
        try
        {
            _cache[key] = (item!, expirationTime);
            _logger.LogInformation("Cache item with key {Key} set with expiration {Expiration}.", key, expirationTime);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
    public void RemoveItem(string key)
    {
        _lock.EnterWriteLock();
        try
        {
            if (_cache.Remove(key))
            {
                _logger.LogInformation("Cache item with key {Key} removed.", key);
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }
    public bool ContainsItem(string key)
    {
        _lock.EnterReadLock();
        try
        {
            if (_cache.TryGetValue(key, out var entry))
            {
                if (entry.Expiration == null || entry.Expiration > DateTime.UtcNow)
                {
                    return true;
                }
                else
                {
                    _logger.LogInformation("Cache item with key {Key} has expired.", key);
                    RemoveItem(key);
                }
            }
            return false;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
}