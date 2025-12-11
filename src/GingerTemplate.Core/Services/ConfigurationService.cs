using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GingerTemplate.Core.Services;

/// <summary>
/// Interface for configuration service operations.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
/// services.AddSingleton&lt;IConfigurationService&gt;(sp => new ConfigurationService("config.json", sp.GetRequiredService&lt;ILogger&lt;ConfigurationService&gt;&gt;()));
/// var config = sp.GetRequiredService&lt;IConfigurationService&gt;();
/// var db = config.GetSection&lt;DatabaseSettings&gt;("Database");
/// var timeout = config.GetValue<int>("Api:TimeoutSeconds", 30);
/// </code>
/// </remarks>
public interface IConfigurationService
{
    T GetSection<T>(string sectionName) where T : class;
    T GetSectionOrDefault<T>(string sectionName, T defaultValue) where T : class;
    object? GetValue(string key);
    object? GetValue(string key, object? defaultValue);
    T GetValue<T>(string key);
    T GetValue<T>(string key, T defaultValue);
    bool TryGetSection<T>(string sectionName, out T? section) where T : class;
    bool TryGetValue<T>(string key, out T? value);
    IEnumerable<string> GetKeys();
}

/// <summary>
/// Implementation of configuration service that loads and manages JSON configurations.
/// </summary>
public class ConfigurationService : IConfigurationService
{
    private readonly Dictionary<string, JsonElement> _configuration;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<ConfigurationService> _logger;

    public ConfigurationService(string configPath, ILogger<ConfigurationService> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _logger.LogInformation("ConfigurationService initialized.");
        _configuration = new Dictionary<string, JsonElement>();
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        LoadConfiguration(configPath);
    }

    public ConfigurationService(Dictionary<string, object?> configuration)
    {
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        _configuration = ConvertToJsonElements(configuration);
    }

    public T GetSection<T>(string sectionName) where T : class
    {
        if (TryGetSection(sectionName, out T? section))
        {
            return section!;
        }

        throw new KeyNotFoundException($"Configuration section '{sectionName}' not found.");
    }

    public T GetSectionOrDefault<T>(string sectionName, T defaultValue) where T : class
    {
        if (TryGetSection(sectionName, out T? section))
        {
            return section!;
        }

        return defaultValue;
    }

    public object? GetValue(string key)
    {
        if (TryGetValue<object>(key, out var value))
        {
            return value;
        }

        return null;
    }

    public object? GetValue(string key, object? defaultValue)
    {
        return TryGetValue<object>(key, out var value) ? value : defaultValue;
    }

    public T GetValue<T>(string key)
    {
        if (TryGetValue(key, out T? value))
        {
            return value!;
        }

        throw new KeyNotFoundException($"Configuration key '{key}' not found.");
    }

    public T GetValue<T>(string key, T defaultValue)
    {
        return TryGetValue(key, out T? value) ? value! : defaultValue;
    }

    public bool TryGetSection<T>(string sectionName, out T? section) where T : class
    {
        section = null;

        if (!_configuration.ContainsKey(sectionName))
            return false;

        try
        {
            var element = _configuration[sectionName];
            section = JsonSerializer.Deserialize<T>(element.GetRawText(), _jsonOptions);
            return section != null;
        }
        catch
        {
            return false;
        }
    }

    public bool TryGetValue<T>(string key, out T? value)
    {
        value = default;
        var keys = key.Split(':', StringSplitOptions.RemoveEmptyEntries);

        if (keys.Length == 0)
            return false;

        if (!_configuration.TryGetValue(keys[0], out var element))
            return false;

        for (int i = 1; i < keys.Length; i++)
        {
            if (element.ValueKind != JsonValueKind.Object)
                return false;

            if (!element.TryGetProperty(keys[i], out element))
                return false;
        }

        try
        {
            value = JsonSerializer.Deserialize<T>(element.GetRawText(), _jsonOptions);
            return value != null;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<string> GetKeys()
    {
        return _configuration.Keys;
    }

    private void LoadConfiguration(string configPath)
    {
        if (string.IsNullOrWhiteSpace(configPath) || !File.Exists(configPath))
            return;

        try
        {
            var json = File.ReadAllText(configPath);
            using var doc = JsonDocument.Parse(json);
            foreach (var property in doc.RootElement.EnumerateObject())
            {
                _configuration[property.Name] = property.Value.Clone();
            }
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load configuration from '{configPath}'.", ex);
        }
    }

    private Dictionary<string, JsonElement> ConvertToJsonElements(Dictionary<string, object?> configuration)
    {
        var result = new Dictionary<string, JsonElement>();
        var json = JsonSerializer.Serialize(configuration, _jsonOptions);
        using var doc = JsonDocument.Parse(json);

        foreach (var property in doc.RootElement.EnumerateObject())
        {
            result[property.Name] = property.Value.Clone();
        }

        return result;
    }
}
