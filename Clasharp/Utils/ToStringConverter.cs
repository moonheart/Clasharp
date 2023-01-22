using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace Clasharp.Utils;

public class ToStringConverter: IValueConverter
{
    public static readonly ToStringConverter Instance = new();
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value?.ToString();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}