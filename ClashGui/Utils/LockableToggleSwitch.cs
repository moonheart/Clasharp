using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Styling;

namespace ClashGui.Utils;

[PseudoClasses(":checked", ":unchecked", ":indeterminate")]
public class LockableToggleSwitch : ToggleSwitch, IStyleable
{
    protected override void Toggle()
    {
        RequestChecked = !(IsChecked ?? false);
    }

    public static readonly StyledProperty<bool> RequestCheckedProperty =
        AvaloniaProperty.Register<LockableToggleSwitch, bool>(nameof(RequestChecked), defaultValue: true);

    public bool RequestChecked
    {
        get => GetValue(RequestCheckedProperty);
        set => SetValue(RequestCheckedProperty, value);
    }

    Type IStyleable.StyleKey => typeof(ToggleSwitch);
}