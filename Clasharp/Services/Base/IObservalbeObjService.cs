using System;

namespace Clasharp.Services.Base;

public interface IObservalbeObjService<out T>
{
    IObservable<T> Obj { get; }
}