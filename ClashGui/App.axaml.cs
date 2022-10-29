using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.ViewModels;
using ClashGui.Views;
using Splat;

namespace ClashGui
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            SplatRegistrations.RegisterLazySingleton<IClashCli, ClashCli>();
            SplatRegistrations.RegisterLazySingleton<IMainWindowViewModel, MainWindowViewModel>();
            SplatRegistrations.RegisterLazySingleton<IProxiesViewModel, ProxiesViewModel>();
            SplatRegistrations.RegisterLazySingleton<IClashLogsViewModel, ClashLogsViewModel>();
            SplatRegistrations.RegisterLazySingleton<IProxyRulesListViewModel, ProxyRulesListViewModel>();
            SplatRegistrations.RegisterLazySingleton<IConnectionsViewModel, ConnectionsViewModel>();
            SplatRegistrations.RegisterLazySingleton<IClashInfoViewModel, ClashInfoViewModel>();
            SplatRegistrations.RegisterLazySingleton<IProxyGroupListViewModel, ProxyGroupListViewModel>();
            SplatRegistrations.RegisterLazySingleton<IProxyProviderListViewModel, ProxyProviderListViewModel>();
            SplatRegistrations.RegisterLazySingleton<IDashboardViewModel, DashboardViewModel>();
            SplatRegistrations.SetupIOC();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = Locator.Current.GetService<IMainWindowViewModel>(),
                    ClashCli =  Locator.Current.GetService<IClashCli>()
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}