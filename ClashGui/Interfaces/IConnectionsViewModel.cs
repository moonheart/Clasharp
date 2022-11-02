using System.Collections.ObjectModel;
using System.Reactive;
using ClashGui.Models.Connections;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IConnectionsViewModel: IViewModelBase
{
    public string DownloadTotal { get; }
    public string UploadTotal { get; }

    public string DownloadSpeed { get; }
    public string UploadSpeed { get; }
    public ReadOnlyObservableCollection<ConnectionExt> Connections { get; }

    public ConnectionExt? SelectedItem { get; set; }
    
    ReactiveCommand<string, Unit> CloseConnection { get; }
    ReactiveCommand<Unit, Unit> CloseAllConnection { get; }
}