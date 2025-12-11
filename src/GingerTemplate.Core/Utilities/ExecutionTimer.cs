using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GingerTemplate.Core.Utilities;

/// <summary>
/// Helpers for measuring execution time of code blocks.
/// </summary>
public static class ExecutionTimer
{
    public static (TimeSpan elapsed, T result) Measure<T>(Func<T> action)
    {
        var stopwatch = Stopwatch.StartNew();
        var result = action();
        stopwatch.Stop();
        return (stopwatch.Elapsed, result);
    }

    public static TimeSpan Measure(Action action)
    {
        var stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }

    public static async Task<(TimeSpan elapsed, T result)> MeasureAsync<T>(Func<Task<T>> action)
    {
        var stopwatch = Stopwatch.StartNew();
        var result = await action().ConfigureAwait(false);
        stopwatch.Stop();
        return (stopwatch.Elapsed, result);
    }

    public static async Task<TimeSpan> MeasureAsync(Func<Task> action)
    {
        var stopwatch = Stopwatch.StartNew();
        await action().ConfigureAwait(false);
        stopwatch.Stop();
        return stopwatch.Elapsed;
    }
}
