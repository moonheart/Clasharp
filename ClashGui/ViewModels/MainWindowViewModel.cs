namespace ClashGui.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Greeting => "Welcome to Avalonia!";

        public ProxyListViewModel ProxyListViewModel => new ();

        public ClashLogsViewModel ClashLogsViewModel => new();
        
        public ProxyRulesListViewModel ProxyRulesListViewModel => new();
    }
}