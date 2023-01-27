using System;
using System.Collections.Generic;

namespace Clasharp.Utils;

public static class YamlUtils
{
    public static Dictionary<object, object> Patch<T>(this Dictionary<object, object> yaml, string path, T value)
    {
        var paths = path.Split('.', StringSplitOptions.RemoveEmptyEntries);

        Dictionary<object, object> current = yaml;
        for (var i = 0; i < paths.Length; i++)
        {
            var p = paths[i];
            if (i == paths.Length - 1)
            {
                current[p] = value;
                continue;
            }
            if (current.ContainsKey(p))
            {
                if (current[p] is Dictionary<object, object> x)
                {
                    current = x;
                }
                else
                {
                    throw new InvalidOperationException($"config patch failed, {path}");
                }
            }
            else
            {
                current[p] = new Dictionary<object, object>();
            }
        }

        return yaml;
    }
}