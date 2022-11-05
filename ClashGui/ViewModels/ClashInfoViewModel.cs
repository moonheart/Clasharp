using System.Reactive.Linq;
using ClashGui.Interfaces;
using ClashGui.Services;
using ClashGui.Utils;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class ClashInfoViewModel : ViewModelBase, IClashInfoViewModel
{
    public ClashInfoViewModel(IRealtimeTrafficService realtimeTrafficService, IVersionService versionService)
    {
        realtimeTrafficService.Obj.Select(d => $"↑ {d.Up.ToHumanSize()}/s\n↓ {d.Down.ToHumanSize()}/s")
            .ToPropertyEx(this, d => d.RealtimeSpeed);

        versionService.Obj.Select(d => $"{d.Version}\n{(d.Premium ? "Premium" : "")}")
            .ToPropertyEx(this, d => d.Version);
    }

    [ObservableAsProperty]
    public string Version { get; }

    [ObservableAsProperty]
    public string RealtimeSpeed { get; }
}