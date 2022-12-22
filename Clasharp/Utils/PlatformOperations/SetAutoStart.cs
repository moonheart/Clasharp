using System;
using System.IO;
using System.Threading.Tasks;

namespace Clasharp.Utils.PlatformOperations;

public class SetAutoStart: PlatformSpecificOperation<string, int>
{
    /// <summary>
    /// Set auto start
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public override Task<int> Exec(string path)
    {
        return base.Exec(path);
    }

    protected override async Task<int> DoForLinux(string path)
    {
        var desktop = $@"
[Desktop Entry]
Categories=Utility;
Comment=Clasharp, A gui for clash-meta
Exec=""{path}"" --autostart
Name=Clasharp
Terminal=false
Type=Application
";
        await File.WriteAllTextAsync(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config/autostart/clasharp.desktop"), desktop);
        return 0;
    }
}