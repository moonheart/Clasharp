using Autofac;
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
using Splat.Autofac;

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

            // https://www.reactiveui.net/docs/handbook/dependency-inversion/custom-dependency-inversion
            var builder = new ContainerBuilder();
            builder.RegisterType<ClashCli>().As<IClashCli>();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            builder.RegisterType<ProxiesViewModel>().As<IProxiesViewModel>();
            builder.RegisterType<ClashLogsViewModel>().As<IClashLogsViewModel>();
            builder.RegisterType<ProxyRulesListViewModel>().As<IProxyRulesListViewModel>();
            builder.RegisterType<ConnectionsViewModel>().As<IConnectionsViewModel>();
            builder.RegisterType<ClashInfoViewModel>().As<IClashInfoViewModel>();
            builder.RegisterType<ProxyGroupListViewModel>().As<IProxyGroupListViewModel>();
            builder.RegisterType<ProxyProviderListViewModel>().As<IProxyProviderListViewModel>();
            builder.RegisterType<DashboardViewModel>().As<IDashboardViewModel>();
            builder.RegisterInstance(state).As<ISettingsViewModel>();

            var autofacDependencyResolver = builder.UseAutofacDependencyResolver();
            builder.RegisterInstance(autofacDependencyResolver);
            autofacDependencyResolver.InitializeReactiveUI();

            var container = builder.Build();
            var autofacResolver = container.Resolve<AutofacDependencyResolver>();
            autofacResolver.SetLifetimeScope(container);

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