namespace ClashGui.Interfaces;

public interface IClashInfoViewModel: IViewModelBase
{
    string Version { get; }
    
    string RealtimeSpeed { get; }
}