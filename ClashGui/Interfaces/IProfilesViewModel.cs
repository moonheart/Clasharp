using System.Collections.ObjectModel;
using System.Reactive;
using ClashGui.Models.Profiles;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProfilesViewModel : IViewModelBase
{
    ReadOnlyObservableCollection<Profile> Profiles { get; }

    ReactiveCommand<Profile, Unit> OpenCreateBox { get; }
}