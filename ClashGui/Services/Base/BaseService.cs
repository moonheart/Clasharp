using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ClashGui.Cli;

namespace ClashGui.Services.Base;

public abstract class BaseService<T>: IAutoFreshable, IObservalbeObjService<T>
{
    public bool EnableAutoFresh { get; set; } = true;

    public IObservable<T> Obj { get; }
    private readonly T? _obj = default;


    protected BaseService(IClashCli clashCli)
    {
        Obj = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .CombineLatest(clashCli.RunningObservable)
            .Where(tuple => tuple.Second == RunningState.Started && EnableAutoFresh)
            .SelectMany(_=>GetObj())
            .Where(items => _obj == null || !ObjEquals(_obj, items));
    }

    protected abstract Task<T> GetObj();

    protected abstract bool ObjEquals(T oldObj, T newObj);
}