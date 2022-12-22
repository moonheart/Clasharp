using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using MessageBox.Avalonia;

namespace Clasharp;

public static class MessageBox
{
    public static async Task Show(string title, string text)
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            await MessageBoxManager.GetMessageBoxStandardWindow(
                    title,
                    text,
                    windowStartupLocation: WindowStartupLocation.CenterOwner)
                .ShowDialog(desktop.MainWindow);
        }
    }
}