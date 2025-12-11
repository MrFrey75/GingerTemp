using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GingerTemplate.Core.Services;

public interface IFileStorageService
{
    void SaveFile(string path, byte[] content);
    byte[]? GetFile(string path);
    void DeleteFile(string path);
    bool FileExists(string path);
}

public class FileStorageService : IFileStorageService
{
    private readonly ILogger<FileStorageService> _logger;
    private readonly ReaderWriterLockSlim _lock = new();

    public FileStorageService(ILogger<FileStorageService> logger)
    {
        _logger = logger;
    }

    public void SaveFile(string path, byte[] content)
    {
        _lock.EnterWriteLock();
        try
        {
            System.IO.File.WriteAllBytes(path, content);
            _logger.LogInformation("File saved at {Path}.", path);
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public byte[]? GetFile(string path)
    {
        _lock.EnterReadLock();
        try
        {
            if (System.IO.File.Exists(path))
            {
                _logger.LogInformation("File retrieved from {Path}.", path);
                return System.IO.File.ReadAllBytes(path);
            }
            else
            {
                _logger.LogWarning("File at {Path} does not exist.", path);
                return null;
            }
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }

    public void DeleteFile(string path)
    {
        _lock.EnterWriteLock();
        try
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
                _logger.LogInformation("File at {Path} deleted.", path);
            }
            else
            {
                _logger.LogWarning("File at {Path} does not exist. Cannot delete.", path);
            }
        }
        finally
        {
            _lock.ExitWriteLock();
        }
    }

    public bool FileExists(string path)
    {
        _lock.EnterReadLock();
        try
        {
            var exists = System.IO.File.Exists(path);
            _logger.LogInformation("File existence check at {Path}: {Exists}.", path, exists);
            return exists;
        }
        finally
        {
            _lock.ExitReadLock();
        }
    }
}
