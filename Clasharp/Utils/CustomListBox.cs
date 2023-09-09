using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Generators;
using Avalonia.Input;
using Avalonia.Styling;
using Avalonia.VisualTree;

namespace Clasharp.Utils;

public class CustomListBoxItem : ListBoxItem
{
    protected override Type StyleKeyOverride => typeof(ListBoxItem);
}

public class CustomListBox : ListBox
{
    protected override void OnPointerPressed(PointerPressedEventArgs e)
    {
        if (e.Source is Visual source)
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
    protected override Type StyleKeyOverride => typeof(ListBox);


    // protected override IItemContainerGenerator CreateItemContainerGenerator()
    // {
    //     return new ItemContainerGenerator<CustomListBoxItem>(
    //         this, 
    //         CustomListBoxItem.ContentProperty,
    //         CustomListBoxItem.ContentTemplateProperty);
    // }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        base.OnPointerReleased(e);
    }
}