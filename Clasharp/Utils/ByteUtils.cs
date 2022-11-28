namespace Clasharp.Utils;

public static class ByteUtils
{
    private static readonly string[] _sizes = {"B", "KB", "MB", "GB", "TB"};

    public static string ToHumanSize(this long bytes)
    {
        int order = 0;
        var len = bytes * 1.0;
        while (len >= 1024 && order < _sizes.Length - 1)
        {
            order++;
            len /= 1024.0;
        }

        return $"{len:0.##} {_sizes[order]}";
    }
}