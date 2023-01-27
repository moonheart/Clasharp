using System;
using System.Collections.Generic;
using Clasharp.Cli;
using DynamicData;

namespace Clasharp.Services.Base;

public abstract class BaseListService<T, TKey> : BaseService<List<T>>, IObservableListService<T, TKey>
    where TKey : notnull
{
    private readonly SourceCache<T, TKey> _items;
    public IObservable<IChangeSet<T, TKey>> List => _items.Connect();

    protected BaseListService(IClashCli clashCli, IClashApiFactory clashApiFactory) : base(clashApiFactory, clashCli)
    {
        _clashApiFactory = clashApiFactory;
        _items = new SourceCache<T, TKey>(GetUniqueKey);
        IObservable<List<T>> observable = GetObservable();
        observable.Subscribe(d => { _items.EditDiff(d, EqualityComparer<T>.Default); });
    }

    protected abstract TKey GetUniqueKey(T obj);

}