using System;
using System.Collections.Generic;
using ClashGui.Cli;
using DynamicData;

namespace ClashGui.Services.Base;

public abstract class BaseListService<T, TKey> : BaseService<List<T>>, IObservableListService<T, TKey>
    where TKey : notnull
{
    private readonly SourceCache<T, TKey> _items;
    public IObservable<IChangeSet<T, TKey>> List => _items.Connect();

    protected BaseListService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashApiFactory, clashCli)
    {
        _clashApiFactory = clashApiFactory;
        _items = new SourceCache<T, TKey>(GetUniqueKey);
        GetObservable().Subscribe(d => { _items.EditDiff(d, EqualityComparer<T>.Default); });
    }

    // protected override bool ObjEquals(List<T> oldObj, List<T> newObj)
    // {
    //     return oldObj.SequenceEqual(newObj);
    // }

    protected abstract TKey GetUniqueKey(T obj);

    // protected abstract Task<List<T>> GetObj();
}