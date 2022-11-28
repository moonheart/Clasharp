using System;
using System.Reactive.Linq;
using Clasharp.Cli;

namespace Clasharp.Services.Base;

public abstract class BaseObjService<T> : BaseService<T>, IObservalbeObjService<T>
{
    public IObservable<T> Obj { get; }
    private T? _obj;

    protected BaseObjService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashApiFactory, clashCli)
    {
        Obj = GetObservable().Where(items =>
        {
            if (_obj != null && ObjEquals(_obj, items)) return false;
            _obj = items;
            return true;
        });
    }

    protected virtual bool ObjEquals(T oldObj, T newObj)
    {
        return Equals(oldObj, newObj);
    }
}