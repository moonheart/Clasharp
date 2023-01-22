using System.ComponentModel;
using RichEnum.Attribute;

namespace Clasharp.Cli;

[RichEnum(EnableLocalization = true, ResourceManager = "Clasharp.Resources.ResourceManager")]
public enum RunningState
{
    [Description("txtStopped")]
    Stopped,
    [Description("txtStarting")]
    Starting,
    [Description("txtStarted")]
    Started,
    [Description("txtStopping")]
    Stopping
}