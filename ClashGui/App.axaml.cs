using Autofac;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
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
            builder.RegisterType<ClashCli>().As<IClashCli>().SingleInstance();
            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().SingleInstance();
            builder.RegisterType<ProxiesViewModel>().As<IProxiesViewModel>().SingleInstance();
            builder.RegisterType<ClashLogsViewModel>().As<IClashLogsViewModel>().SingleInstance();
            builder.RegisterType<ProxyRulesListViewModel>().As<IProxyRulesListViewModel>().SingleInstance();
            builder.RegisterType<ConnectionsViewModel>().As<IConnectionsViewModel>().SingleInstance();
            builder.RegisterType<ClashInfoViewModel>().As<IClashInfoViewModel>().SingleInstance();
            builder.RegisterType<ProxyGroupListViewModel>().As<IProxyGroupListViewModel>().SingleInstance();
            builder.RegisterType<ProxyProviderListViewModel>().As<IProxyProviderListViewModel>().SingleInstance();
            builder.RegisterType<DashboardViewModel>().As<IDashboardViewModel>().SingleInstance();
            builder.RegisterInstance(state).As<ISettingsViewModel>().SingleInstance();

            var autofacDependencyResolver = builder.UseAutofacDependencyResolver();
            builder.RegisterInstance(autofacDependencyResolver);
            autofacDependencyResolver.InitializeReactiveUI();
            
            // https://github.com/reactiveui/splat/issues/882
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
            Locator.CurrentMutable.RegisterConstant(new AvaloniaActivationForViewFetcher(), typeof(IActivationForViewFetcher));
            Locator.CurrentMutable.RegisterConstant(new AutoDataTemplateBindingHook(), typeof(IPropertyBindingHook));

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