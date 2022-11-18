using System.Collections.ObjectModel;
using System.Reactive;
using ClashGui.Models.Profiles;
using ReactiveUI;

namespace ClashGui.Interfaces;

public interface IProfilesViewModel : IViewModelBase
{
    ReadOnlyObservableCollection<Profile> Profiles { get; }

    Profile? SelectedProfile { get; set; }
    
    ReactiveCommand<Profile, Unit> OpenCreateBox { get; }
}