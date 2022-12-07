using System.Reactive;
using Clasharp.Interfaces;
using Clasharp.ViewModels;
using ReactiveUI;

namespace Clasharp.DesignTime;

public class DesignClashCoreManageViewModel: ViewModelBase, IClashCoreManageViewModel
{
    public string CurrentVersion { get; }
    public bool UseCustomUrl { get; set; }
    public string CustomUrl { get; set; }
    public ReactiveCommand<Unit, Unit> Download { get; }
    public bool IsDownloading { get; }
}