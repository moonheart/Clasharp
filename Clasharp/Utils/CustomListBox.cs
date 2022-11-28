using System;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Input;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace Clasharp.Utils;

public class CustomListBoxItem : ListBoxItem, IStyleable
{
    Type IStyleable.StyleKey => typeof(ListBoxItem);
}

public class CustomListBox : ListBox, IStyleable
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
    Type IStyleable.StyleKey => typeof(ListBox);

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