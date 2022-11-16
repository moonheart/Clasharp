using System;
using DynamicData;

namespace ClashGui.Services.Base;

public interface IObservableListService<T, TKey> where TKey : notnull
{
    IObservable<IChangeSet<T, TKey>> List { get; }
}