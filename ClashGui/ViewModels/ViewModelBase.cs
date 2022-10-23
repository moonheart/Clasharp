using ClashGui.Interfaces;
using ReactiveUI;

namespace ClashGui.ViewModels
{
    public class ViewModelBase : ReactiveObject, IViewModelBase
    {
        public string Name { get; set; }

        public ViewModelBase()
        {
            Name = this.GetType().Name.Replace("ViewModel", "");
        }
    }
}