using System.Reactive;
using System.Threading.Tasks;
using ClashGui.Cli;
using ClashGui.Cli.ClashConfigs;
using ClashGui.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ClashGui.ViewModels;

public class DashboardViewModel:ViewModelBase, IDashboardViewModel
{
    private IClashCli _clashCli;
    
    public DashboardViewModel(IClashCli clashCli)
    {
        _clashCli = clashCli;

        StartClash = ReactiveCommand.CreateFromTask(async () =>
        {
            var rawConfig = await _clashCli.Start();
            _rawConfig = rawConfig;
        });

        StopClash = ReactiveCommand.CreateFromTask(async () =>
        {
            await _clashCli.Stop();
        });

        _clashCli.RunningObservable.BindTo(this, d => d.RunningState);
    }
    
    [Reactive]
    private RawConfig _rawConfig { get; set; }
    
    public override string Name => "Dashboard";
    public ReactiveCommand<Unit, Unit> StartClash { get; }
    public ReactiveCommand<Unit, Unit> StopClash { get; }
    
    [Reactive]
    public RunningState RunningState { get; set; }
}