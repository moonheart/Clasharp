using System;
using System.Linq;
using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Themes.Fluent;
using Avalonia.Threading;
using Clasharp.Cli;
using Clasharp.Interfaces;
using Clasharp.Models.Settings;
using Clasharp.Services;
using Clasharp.Utils;
using Clasharp.ViewModels;
using Clasharp.Views;
using Clasharp.Common;
using ReactiveUI;
using Splat;
using Splat.Autofac;
using Splat.ModeDetection;

namespace Clasharp
{
    public partial class App : Application
    {
        public App()
        {
            ModeDetector.OverrideModeDetector(Mode.Run);
        }

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
            base.OnFrameworkInitializationCompleted();
        }

        private void SetupLifetime()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.ShutdownRequested += (sender, args) => { Locator.Current.GetService<IClashCli>()?.Stop(); };
                desktop.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                if (desktop.Args == null || desktop.Args.Length <= 0 || desktop.Args.All(d => d != "--autostart"))
                {
                    StartMainWindow(desktop);
                }

                if (desktop.Args != null && desktop.Args.Any(d => d == "--autostart"))
                {
                    Locator.Current.GetService<IClashCli>()?.Start();
                }
            }

            Locator.Current.GetService<AppSettings>().WhenAnyValue(d => d.ThemeMode).Subscribe(theme =>
            {
                if (Styles.FirstOrDefault(d => d is FluentTheme) is FluentTheme fluentTheme)
                {
                    Current!.RequestedThemeVariant = theme;
                }
            });
        }

        private void StartMainWindow(IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow is not { IsVisible: true })
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = Locator.Current.GetService<IMainWindowViewModel>(),
                };
            }

            desktop.MainWindow.Show();
            desktop.MainWindow.Activate();
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
            var suspension = new AutoSuspendHelper(ApplicationLifetime!);
            RxApp.SuspensionHost.CreateNewAppState = () => new AppSettings();
            RxApp.SuspensionHost.SetupDefaultSuspendResume(
                new NewtonsoftJsonSuspensionDriver<AppSettings>(GlobalConfigs.MainConfig));
            suspension.OnFrameworkInitializationCompleted();
        }

        private void OpenMainWindow(object? sender, EventArgs e)
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                StartMainWindow(desktop);
        }
    }
}