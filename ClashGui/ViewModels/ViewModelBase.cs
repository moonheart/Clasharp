using ClashGui.Interfaces;
using ReactiveUI;

namespace ClashGui.ViewModels
{
    public class ViewModelBase : ReactiveObject, IViewModelBase
    {
        public virtual string Name { get; }

    }
}