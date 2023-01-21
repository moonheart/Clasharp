using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Clasharp.Cli;

namespace Clasharp.Services.Base;

public abstract class BaseService<T> : IAutoFreshable
{
    public bool EnableAutoFresh { get; set; } = true;
    protected IClashApiFactory _clashApiFactory;
    protected IClashCli _clashCli;

    protected BaseService(IClashApiFactory clashApiFactory, IClashCli clashCli)
    {
        _clashApiFactory = clashApiFactory;
        _clashCli = clashCli;
    }

    protected abstract Task<T> GetObj();

    protected IObservable<T> GetObservable()
    {
        return Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .CombineLatest(_clashCli.RunningState)
            .Where(tuple => tuple.Second == Cli.Generated.RunningState.Started && EnableAutoFresh)
            .SelectMany(_ => GetObj());
    }
}