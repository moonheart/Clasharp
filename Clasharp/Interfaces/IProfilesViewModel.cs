using System.Collections.ObjectModel;
using System.Reactive;
using Clasharp.Models.Profiles;
using ReactiveUI;

namespace Clasharp.Interfaces;

public interface IProfilesViewModel : IViewModelBase
{
    ReadOnlyObservableCollection<Profile> Profiles { get; }

    Profile? SelectedProfile { get; set; }
    
    ReactiveCommand<Profile?, Unit> OpenCreateBox { get; }
    
    ReactiveCommand<Profile, Unit> DeleteProfile { get; }
    
}