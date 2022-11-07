using Autofac;
using ClashGui.Cli;

namespace ClashGui.Services;

public interface IClashCliFactory
{
    IClashCli Get(bool remote = false);
}

public class ClashCliFactory: IClashCliFactory
{
    private IContainer _container;

    public ClashCliFactory(IContainer container)
    {
        _container = container;
    }

    public IClashCli Get(bool remote = false)
    {
        return _container.ResolveKeyed<IClashCli>(remote);
    }
}