using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using MsBox.Avalonia;

namespace Clasharp;

public static class MessageBox
{
    public static async Task Show(string title, string text)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            await MessageBoxManager.GetMessageBoxStandard(
                    title,
                    text,
                    windowStartupLocation: WindowStartupLocation.CenterOwner)
                .ShowWindowDialogAsync(desktop.MainWindow);
        }
    }
}