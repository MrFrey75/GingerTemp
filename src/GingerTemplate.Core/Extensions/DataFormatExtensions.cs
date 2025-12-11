namespace GingerTemplate.Core.Extensions;

public static class DataFormatExtensions
{
    /// <summary>
    /// Gets the file extension for the given data format.
    /// </summary>
    public static string GetFileExtension(this DataFormat format)
    {
        return format switch
        {
            DataFormat.Json => ".json",
            DataFormat.Xml => ".xml",
            DataFormat.Csv => ".csv",
            DataFormat.Yaml => ".yaml",
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
    }

    /// <summary>
    /// Parses a string to a DataFormat enum.
    /// </summary>
    public static DataFormat Parse(string format)
    {
        return format.ToLower() switch
        {
            "json" => DataFormat.Json,
            "xml" => DataFormat.Xml,
            "csv" => DataFormat.Csv,
            "yaml" => DataFormat.Yaml,
            _ => throw new ArgumentException($"Unknown data format: {format}")
        };  
    }

    /// <summary>
    /// Converts the DataFormat enum to a string.
    /// </summary>
    public static string ToFormatString(this DataFormat format)
    {
        return format switch
        {
            DataFormat.Json => "json",
            DataFormat.Xml => "xml",
            DataFormat.Csv => "csv",
            DataFormat.Yaml => "yaml",
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
    }
}