namespace ClashGui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public ProxyListViewModel ProxyListViewModel => new ();
    }
}