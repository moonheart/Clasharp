using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Styling;

namespace Clasharp.Utils;

[PseudoClasses(":checked", ":unchecked", ":indeterminate")]
public class LockableToggleSwitch : ToggleSwitch
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

    protected override Type StyleKeyOverride => typeof(ToggleSwitch);
}