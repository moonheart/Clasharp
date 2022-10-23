using ClashGui.Interfaces;
using ClashGui.ViewModels;

namespace ClashGui.DesignTime;

public class DesignClashInfoViewModel:ViewModelBase, IClashInfoViewModel
{
    public string Version => "v1111\nPremium";
    public string RealtimeSpeed => "↑ 12KB/s\n↓ 34KB/s";
}