using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ClashGui.Cli;

namespace ClashGui.Services.Base;

public abstract class BaseService<T>: IAutoFreshable, IObservalbeObjService<T>
{
    public bool EnableAutoFresh { get; set; } = true;

    public IObservable<T> Obj { get; }
    private T? _obj;

    protected IClashApiFactory _clashApiFactory;
    protected BaseService(IClashCli clashCli, IClashApiFactory clashApiFactory)
    {
        _clashApiFactory = clashApiFactory;
        Obj = Observable.Timer(TimeSpan.Zero, TimeSpan.FromSeconds(1))
            .CombineLatest(clashCli.RunningState)
            .Where(tuple => tuple.Second == RunningState.Started && EnableAutoFresh)
            .SelectMany(_=>GetObj())
            .Where(items =>
            {
                if (_obj != null && ObjEquals(_obj, items)) return false;
                _obj = items;
                return true;
            });
    }

    protected abstract Task<T> GetObj();

    protected virtual bool ObjEquals(T oldObj, T newObj)
    {
        return Equals(oldObj, newObj);
    }
}

public abstract class BaseListService<T> : BaseService<List<T>>
{
    protected BaseListService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashCli, clashApiFactory)
    {
    }

    protected override bool ObjEquals(List<T> oldObj, List<T> newObj)
    {
        return oldObj.SequenceEqual(newObj);
    }
}