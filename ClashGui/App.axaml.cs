using System;
using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ClashGui.Cli;
using ClashGui.Common;
using ClashGui.Interfaces;
using ClashGui.Models.Settings;
using ClashGui.Services;
using ClashGui.Utils;
using ClashGui.ViewModels;
using ClashGui.Views;
using ReactiveUI;
using Refit;
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

        private void Exit(object? sender, EventArgs e)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.TryShutdown();
            }
        }

        public override void OnFrameworkInitializationCompleted()
        {
            SetupSuspensionHost();
            SetupAutofac();
            SetupLifetime();
            StartMainWindow();
            base.OnFrameworkInitializationCompleted();
        }

        private void SetupLifetime()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.ShutdownRequested += (sender, args) =>
                {
                    Locator.Current.GetService<IClashCli>()?.Stop();
                };
                desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;
            }
        }

        private void StartMainWindow()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                if (desktop.MainWindow is not {IsVisible: true})
                {
                    desktop.MainWindow = new MainWindow
                    {
                        DataContext = Locator.Current.GetService<IMainWindowViewModel>(),
                    };
                }

                desktop.MainWindow.Show();
                desktop.MainWindow.Activate();
            }
        }

        private static void SetupAutofac()
        {
            // https://www.reactiveui.net/docs/handbook/dependency-inversion/custom-dependency-inversion
            var builder = new ContainerBuilder();
            builder.RegisterType<ClashCli>().As<IClashCli>().SingleInstance();
            builder.RegisterType<ClashLocalCli>().Named<IClashCli>("local").SingleInstance();
            builder.RegisterType<ClashRemoteCli>().Named<IClashCli>("remote").SingleInstance();
            builder.RegisterType<ClashApiFactory>().As<IClashApiFactory>().SingleInstance();
            builder.RegisterType<CoreServiceHelper>().SingleInstance();

            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>().SingleInstance();
            builder.RegisterType<ProxiesViewModel>().As<IProxiesViewModel>().SingleInstance();
            builder.RegisterType<ClashLogsViewModel>().As<IClashLogsViewModel>().SingleInstance();
            builder.RegisterType<ProxyRulesListViewModel>().As<IProxyRulesListViewModel>().SingleInstance();
            builder.RegisterType<ConnectionsViewModel>().As<IConnectionsViewModel>().SingleInstance();
            builder.RegisterType<ClashInfoViewModel>().As<IClashInfoViewModel>().SingleInstance();
            builder.RegisterType<ProxyGroupListViewModel>().As<IProxyGroupListViewModel>().SingleInstance();
            builder.RegisterType<ProxyProviderListViewModel>().As<IProxyProviderListViewModel>().SingleInstance();
            builder.RegisterType<DashboardViewModel>().As<IDashboardViewModel>().SingleInstance();
            builder.RegisterType<SettingsViewModel>().As<ISettingsViewModel>().SingleInstance();
            builder.RegisterType<ProfilesViewModel>().As<IProfilesViewModel>().SingleInstance();
            builder.RegisterType<ProfileEditViewModel>().As<IProfileEditViewModel>().SingleInstance();
            builder.RegisterInstance(RxApp.SuspensionHost.GetAppState<AppSettings>()).SingleInstance();
            builder.RegisterInstance(
                RestService.For<IRemoteClash>($"http://localhost:{GlobalConfigs.ClashServicePort}/")).SingleInstance();

            builder.RegisterType<ProxyGroupService>().As<IProxyGroupService>().SingleInstance();
            builder.RegisterType<ProxyProviderService>().As<IProxyProviderService>().SingleInstance();
            builder.RegisterType<ProxyRuleService>().As<IProxyRuleService>().SingleInstance();
            builder.RegisterType<ProxyRuleProviderService>().As<IProxyRuleProviderService>().SingleInstance();
            builder.RegisterType<ConnectionService>().As<IConnectionService>().SingleInstance();
            builder.RegisterType<RealtimeTrafficService>().As<IRealtimeTrafficService>().SingleInstance();
            builder.RegisterType<VersionService>().As<IVersionService>().SingleInstance();
            builder.RegisterType<ProfilesService>().As<IProfilesService>().SingleInstance();

            var autofacDependencyResolver = builder.UseAutofacDependencyResolver();
            builder.RegisterInstance(autofacDependencyResolver);
            autofacDependencyResolver.InitializeReactiveUI();

            // https://github.com/reactiveui/splat/issues/882
            RxApp.MainThreadScheduler = AvaloniaScheduler.Instance;
            Locator.CurrentMutable.RegisterConstant(new AvaloniaActivationForViewFetcher(),
                typeof(IActivationForViewFetcher));
            Locator.CurrentMutable.RegisterConstant(new AutoDataTemplateBindingHook(), typeof(IPropertyBindingHook));

            var container = builder.Build();
            ContainerProvider.Container = container;
            var autofacResolver = container.Resolve<AutofacDependencyResolver>();
            autofacResolver.SetLifetimeScope(container);
        }

        private void SetupSuspensionHost()
        {
            // Create the AutoSuspendHelper.
            var suspension = new AutoSuspendHelper(ApplicationLifetime);
            RxApp.SuspensionHost.CreateNewAppState = () => new AppSettings();
            RxApp.SuspensionHost.SetupDefaultSuspendResume(
                new NewtonsoftJsonSuspensionDriver<AppSettings>(GlobalConfigs.MainConfig));
            suspension.OnFrameworkInitializationCompleted();
        }

        private void TrayIcon_OnClicked(object? sender, EventArgs e)
        {
            StartMainWindow();
        }
    }
}