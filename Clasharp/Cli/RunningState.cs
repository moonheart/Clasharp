using RichEnum.Attribute;

namespace Clasharp.Cli;

[RichEnum]
public enum RunningState
{
    Stopped,
    Starting,
    Started,
    Stopping
}