using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace ClashGui.Utils;

public abstract class Watcher<T>
{
    protected readonly ReplaySubject<T> ReplaySubject;
    private CancellationTokenSource? _cancellationTokenSource;

    protected Watcher(ReplaySubject<T> replaySubject)
    {
        ReplaySubject = replaySubject;
    }

    public void Start(string uri)
    {
        _cancellationTokenSource = new CancellationTokenSource();
        Watch(uri, _cancellationTokenSource.Token).ConfigureAwait(false);
    }
        
    public void Stop()
    {
        if (_cancellationTokenSource == null) return;
        if (_cancellationTokenSource.IsCancellationRequested) return;
        _cancellationTokenSource.Cancel();
    }

    protected abstract Task Watch(string uri, CancellationToken cancellationToken);
}