using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Clasharp.Utils;

public abstract class PlatformSpecificOperation<TResult>
{
    public virtual Task<TResult> Exec()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return DoForWindows();
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return DoForLinux();
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return DoForOsx();
        }

        throw new InvalidOperationException(
            $"Operation no valid on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForWindows()
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForLinux()
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForOsx()
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }
}

public abstract class PlatformSpecificOperation<T1, TResult>
{
    public virtual Task<TResult> Exec(T1 t1)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return DoForWindows(t1);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return DoForLinux(t1);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return DoForOsx(t1);
        }

        throw new InvalidOperationException(
            $"Operation no valid on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForWindows(T1 t1)
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForLinux(T1 t1)
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForOsx(T1 t1)
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }
}

public abstract class PlatformSpecificOperation<T1, T2, TResult>
{
    public virtual Task<TResult> Exec(T1 t1, T2 t2)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return DoForWindows(t1, t2);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return DoForLinux(t1, t2);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return DoForOsx(t1, t2);
        }

        throw new InvalidOperationException(
            $"Operation no valid on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForWindows(T1 t1, T2 t2)
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForLinux(T1 t1, T2 t2)
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForOsx(T1 t1, T2 t2)
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }
}

public abstract class PlatformSpecificOperation<T1, T2, T3, TResult>
{
    public virtual Task<TResult> Exec(T1 t1, T2 t2, T3 t3)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return DoForWindows(t1, t2, t3);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return DoForLinux(t1, t2, t3);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return DoForOsx(t1, t2, t3);
        }

        throw new InvalidOperationException(
            $"Operation no valid on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForWindows(T1 t1, T2 t2, T3 t3)
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForLinux(T1 t1, T2 t2, T3 t3)
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }

    protected virtual Task<TResult> DoForOsx(T1 t1, T2 t2, T3 t3)
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");
    }
}