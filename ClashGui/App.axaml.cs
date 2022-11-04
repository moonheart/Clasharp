using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ClashGui.Cli;
using ClashGui.Interfaces;
using ClashGui.Utils;
using ClashGui.ViewModels;
using ClashGui.Views;
using ReactiveUI;
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
            // Create the AutoSuspendHelper.
            var suspension = new AutoSuspendHelper(ApplicationLifetime);
            RxApp.SuspensionHost.CreateNewAppState = () => new SettingsViewModel();
            RxApp.SuspensionHost.SetupDefaultSuspendResume(new NewtonsoftJsonSuspensionDriver(GlobalConfigs.MainConfig));
            suspension.OnFrameworkInitializationCompleted();
            
            var state = RxApp.SuspensionHost.GetAppState<SettingsViewModel>();
            
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
            Locator.CurrentMutable.RegisterConstant<ISettingsViewModel>(state);
            SplatRegistrations.RegisterLazySingleton<ISettingsViewModel, SettingsViewModel>();
            SplatRegistrations.SetupIOC();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = Locator.Current.GetService<IMainWindowViewModel>(),
                    ClashCli =  Locator.Current.GetService<IClashCli>()
                };
                desktop.ShutdownRequested += (sender, args) =>
                {
                    RxApp.SuspensionHost.AppState = Locator.Current.GetService<ISettingsViewModel>();
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}