using System;

namespace ClashGui.Services.Base;

public interface IObservalbeObjService<T>
{
    IObservable<T> Obj { get; }
}