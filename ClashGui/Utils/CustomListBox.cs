using System.Collections;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Mixins;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.VisualTree;

namespace ClashGui.Utils;

public class CustomListBoxItem : ListBoxItem
{
    
}

public class CustomListBox : ListBox
{
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        if (e.Source is IVisual source)
        {
            var point = e.GetCurrentPoint(source);
            if (point.Properties.IsRightButtonPressed)
            {
                e.Handled = true;
                return;
            }
        }
        base.OnPointerPressed(e);
    }

    protected override IItemContainerGenerator CreateItemContainerGenerator()
    {
        return new ItemContainerGenerator<CustomListBoxItem>(
            this, 
            CustomListBoxItem.ContentProperty,
            CustomListBoxItem.ContentTemplateProperty);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
    }
}