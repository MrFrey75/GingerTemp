using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

/// <summary>
/// Manages application theme selection and retrieval.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
/// services.AddSingleton&lt;IThemeManagementService, ThemeManagementService&gt;();
/// var themes = provider.GetRequiredService&lt;IThemeManagementService&gt;();
/// themes.ApplyTheme("Dark");
/// var current = themes.GetCurrentTheme();
/// </code>
/// </remarks>
public interface IThemeManagementService
{
    void ApplyTheme(string themeName);
    string GetCurrentTheme();
}

public class ThemeManagementService : IThemeManagementService
{
    private readonly ILogger<ThemeManagementService> _logger;
    private string _currentTheme = "Default";
    private readonly ReaderWriterLockSlim _lock = new();

    public ThemeManagementService(ILogger<ThemeManagementService> logger)
    {
        _logger = logger;
        _logger.LogInformation("ThemeManagementService initialized.");
    }

    public void ApplyTheme(string themeName)
    {
        _lock.EnterWriteLock();
        try
        {
            _currentTheme = themeName;
            _logger.LogInformation("Theme applied: {ThemeName}.", themeName);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public string GetCurrentTheme()
    {
        _lock.EnterReadLock();
        try
        {
            _logger.LogInformation("Retrieving current theme: {CurrentTheme}.", _currentTheme);
            return _currentTheme;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
}
