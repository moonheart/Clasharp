using Clasharp.Interfaces;
using ReactiveUI;

namespace Clasharp.ViewModels
{
    public class ViewModelBase : ReactiveObject, IViewModelBase
    {
        public virtual string Name { get; }

    }
}