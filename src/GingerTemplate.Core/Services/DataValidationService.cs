using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

/// <summary>
/// Provides basic data validation helpers for common primitive checks.
/// </summary>
/// <remarks>
/// Usage:
/// <code>
/// services.AddSingleton&lt;IDataValidationService, DataValidationService&gt;();
/// var validator = provider.GetRequiredService&lt;IDataValidationService&gt;();
/// var ok = validator.ValidateData(userInput);
/// </code>
/// </remarks>
public interface IDataValidationService
{
    bool ValidateData<T>(T data);
}

public class DataValidationService : IDataValidationService
{
    private readonly ILogger<DataValidationService> _logger;

    public DataValidationService(ILogger<DataValidationService> logger)
    {
        _logger = logger;
        _logger.LogInformation("DataValidationService initialized.");
    }

    public bool ValidateData<T>(T data)
    {
        if (data == null)
        {
            _logger.LogWarning("Data validation failed: data is null.");
            return false;
        }

        // Example validation logic (can be extended as needed)
        var isValid = true;

        if (data is string strData)
        {
            isValid = !string.IsNullOrWhiteSpace(strData);
        }
        else if (data is int intData)
        {
            isValid = intData >= 0; // Example: integers must be non-negative
        }

        if (!isValid)
        {
            _logger.LogWarning("Data validation failed for data of type {DataType}.", typeof(T).Name);
        }
        else
        {
            _logger.LogInformation("Data validation succeeded for data of type {DataType}.", typeof(T).Name);
        }

        return isValid;
    }
}