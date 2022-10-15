using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data;
using Avalonia.Data.Converters;
using ClashGui.Clash.Models.Proxies;

namespace ClashGui.ViewModels;

public class ProxyInfoViewModel : ViewModelBase, IValueConverter
{
    public ProxyGroup ProxyGroup { get; set; }
    
    public ProxyHistory? LatestHistory => ProxyGroup.History.LastOrDefault();


    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ProxyGroup proxyGroup)
        {
            return new ProxyInfoViewModel
            {
                ProxyGroup = proxyGroup
            };
        }

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}