using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using ClashGui.Clash.Models.Providers;

namespace ClashGui.ViewModels;

public class ProxyProviderViewModel : ViewModelBase, IValueConverter
{
    public ProxyProviderViewModel()
    {
    }

    public ProxyProvider Provider { get; set; } = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ProxyProvider proxyProvider)
        {
            return new ProxyProviderViewModel
            {
                Provider = proxyProvider
            };
        }

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}