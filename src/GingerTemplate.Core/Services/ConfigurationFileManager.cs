using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GingerTemplate.Core.Services;

/// <summary>
/// Manager for loading configuration files from a directory.
/// </summary>
public class ConfigurationFileManager
{
    private readonly string _configDirectory;
    private readonly JsonSerializerOptions _jsonOptions;

    public ConfigurationFileManager(string configDirectory)
    {
        _configDirectory = configDirectory ?? throw new ArgumentNullException(nameof(configDirectory));
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        EnsureDirectoryExists();
    }

    /// <summary>
    /// Loads a configuration file and returns a typed object.
    /// </summary>
    public T LoadConfigurationFile<T>(string fileName) where T : class
    {
        var filePath = Path.Combine(_configDirectory, fileName);

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Configuration file '{fileName}' not found in '{_configDirectory}'.");

        try
        {
            var json = File.ReadAllText(filePath);
            var config = JsonSerializer.Deserialize<T>(json, _jsonOptions);
            return config ?? throw new InvalidOperationException($"Failed to deserialize '{fileName}'.");
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load configuration from '{fileName}'.", ex);
        }
    }

    /// <summary>
    /// Tries to load a configuration file with a default fallback.
    /// </summary>
    public bool TryLoadConfigurationFile<T>(string fileName, out T? configuration) where T : class
    {
        configuration = null;
        var filePath = Path.Combine(_configDirectory, fileName);

        if (!File.Exists(filePath))
            return false;

        try
        {
            var json = File.ReadAllText(filePath);
            configuration = JsonSerializer.Deserialize<T>(json, _jsonOptions);
            return configuration != null;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Saves a configuration object to a file.
    /// </summary>
    public async Task SaveConfigurationFileAsync<T>(string fileName, T configuration) where T : class
    {
        var filePath = Path.Combine(_configDirectory, fileName);

        try
        {
            var json = JsonSerializer.Serialize(configuration, _jsonOptions);
            await File.WriteAllTextAsync(filePath, json).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to save configuration to '{fileName}'.", ex);
        }
    }

    /// <summary>
    /// Gets all configuration files in the directory.
    /// </summary>
    public IEnumerable<string> GetConfigurationFiles(string searchPattern = "*.json")
    {
        if (!Directory.Exists(_configDirectory))
            return Array.Empty<string>();

        return Directory.GetFiles(_configDirectory, searchPattern);
    }

    /// <summary>
    /// Checks if a configuration file exists.
    /// </summary>
    public bool ConfigurationFileExists(string fileName)
    {
        var filePath = Path.Combine(_configDirectory, fileName);
        return File.Exists(filePath);
    }

    private void EnsureDirectoryExists()
    {
        if (!Directory.Exists(_configDirectory))
        {
            Directory.CreateDirectory(_configDirectory);
        }
    }
}
