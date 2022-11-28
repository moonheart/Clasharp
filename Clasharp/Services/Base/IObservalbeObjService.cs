using System;

namespace Clasharp.Services.Base;

public interface IObservalbeObjService<T>
{
    IObservable<T> Obj { get; }
}