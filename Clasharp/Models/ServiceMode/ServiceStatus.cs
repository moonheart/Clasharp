namespace Clasharp.Models.ServiceMode;

public enum ServiceStatus
{
    Uninstalled,
    Stopped,
    StartPending,
    StopPending,
    Running,
    ContinuePending,
    PausePending,
    Paused,
}