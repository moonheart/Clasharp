using System;
using Clasharp.ViewModels;
using DynamicData;
using DynamicData.Binding;

namespace Clasharp.Utils;

public static class ObservableExtensions
{
    /// <summary>
    /// Binds the results to the specified readonly observable collection using the default update algorithm.
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="readOnlyObservableCollection">The resulting read only observable collection.</param>
    /// <param name="adaptor">Specify an adaptor to change the algorithm to update the target collection.</param>
    /// <returns>An observable which will emit change sets.</returns>
    /// <exception cref="System.ArgumentNullException">source.</exception>
    public static IObservable<IChangeSet<TObject, TKey>> Bind<TObject, TKey>(
        this IObservable<IChangeSet<TObject, TKey>> source, 
        out MyReadOnlyObservableCollection<TObject> readOnlyObservableCollection,
        int resetThreshold = 25,
        bool useReplaceForUpdates = false,
        IObservableCollectionAdaptor<TObject, TKey>? adaptor = null)
        where TKey : notnull
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        var target = new ObservableCollectionExtended<TObject>();
        var result = new MyReadOnlyObservableCollection<TObject>(target);
        var updater = adaptor ?? new ObservableCollectionAdaptor<TObject, TKey>(resetThreshold, useReplaceForUpdates);
        readOnlyObservableCollection = result;
        return source.Bind(target, updater);
    }
}