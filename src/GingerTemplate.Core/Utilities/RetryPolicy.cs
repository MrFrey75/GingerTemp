using System;
using System.Threading;
using System.Threading.Tasks;

namespace GingerTemplate.Core.Utilities;

/// <summary>
/// Simple retry helper with exponential backoff.
/// </summary>
public static class RetryPolicy
{
    public static async Task<T> ExecuteAsync<T>(Func<Task<T>> action, int maxAttempts = 3, Func<int, TimeSpan>? backoff = null, CancellationToken cancellationToken = default)
    {
        if (maxAttempts <= 0) throw new ArgumentOutOfRangeException(nameof(maxAttempts));
        backoff ??= attempt => TimeSpan.FromMilliseconds(Math.Pow(2, attempt) * 100);

        Exception? lastError = null;
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            cancellationToken.ThrowIfCancellationRequested();
            try
            {
                return await action().ConfigureAwait(false);
            }
            catch (Exception ex) when (attempt < maxAttempts)
            {
                lastError = ex;
                await Task.Delay(backoff(attempt), cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                lastError = ex;
                break;
            }
        }

        throw lastError ?? new InvalidOperationException("RetryPolicy encountered an unknown error.");
    }

    public static async Task ExecuteAsync(Func<Task> action, int maxAttempts = 3, Func<int, TimeSpan>? backoff = null, CancellationToken cancellationToken = default)
    {
        await ExecuteAsync<object?>(async () => { await action().ConfigureAwait(false); return null; }, maxAttempts, backoff, cancellationToken).ConfigureAwait(false);
    }

    public static T Execute<T>(Func<T> action, int maxAttempts = 3, Func<int, TimeSpan>? backoff = null)
    {
        if (maxAttempts <= 0) throw new ArgumentOutOfRangeException(nameof(maxAttempts));
        backoff ??= attempt => TimeSpan.FromMilliseconds(Math.Pow(2, attempt) * 100);

        Exception? lastError = null;
        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                return action();
            }
            catch (Exception ex) when (attempt < maxAttempts)
            {
                lastError = ex;
                Thread.Sleep(backoff(attempt));
            }
            catch (Exception ex)
            {
                lastError = ex;
                break;
            }
        }

        throw lastError ?? new InvalidOperationException("RetryPolicy encountered an unknown error.");
    }

    public static void Execute(Action action, int maxAttempts = 3, Func<int, TimeSpan>? backoff = null)
    {
        Execute<object?>(() => { action(); return null; }, maxAttempts, backoff);
    }
}
