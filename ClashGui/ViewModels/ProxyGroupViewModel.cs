using System;
using System.Globalization;
using System.Linq;
using Avalonia.Data;
using Avalonia.Data.Converters;
using ClashGui.Clash.Models.Proxies;
using ReactiveUI;

namespace ClashGui.ViewModels;

public class ProxyGroupViewModel : ViewModelBase, IValueConverter
{
    public ProxyGroupViewModel()
    {
    }

    public ProxyGroup Group { get; set; } = null!;

    public int? SelectedIndex => Group.Now == null ? null : Group.All.IndexOf(Group.Now);

    // public static readonly IValueConverter Instance = new ProxyGroupViewModel();
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is ProxyGroup proxyGroup)
        {
            return new ProxyGroupViewModel
            {
                Group = proxyGroup
            };
        }

        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}