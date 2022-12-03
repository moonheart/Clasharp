using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Clasharp.Utils;

public abstract class PlatformSpecificOperation<TResult>
{
    public Task<TResult> Do()
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
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");    }
    protected virtual Task<TResult> DoForOsx()
    {
        throw new InvalidOperationException(
            $"Operation no implemented on current platform: {RuntimeInformation.OSDescription}");    }
    
}