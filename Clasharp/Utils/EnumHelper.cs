using System;
using System.Collections.Generic;
using System.Linq;

namespace Clasharp.Utils;

public static class EnumHelper
{
    public static IEnumerable<T> GetAllEnumValues<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>();
    }

    public static IEnumerable<T> GetEnumValues<T>(params T[] exclude) where T : Enum
    {
        foreach (T item in Enum.GetValues(typeof(T)))
        {
            if (exclude != null && !exclude.Contains(item))
            {
                yield return item;
            }
        }
    }
}