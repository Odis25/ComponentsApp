using ComponentsApp.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace ComponentsApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _host;
        public static IHost Host  => _host ?? Program.CreateHostBuilder(Environment.GetCommandLineArgs()); 
        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowVm>();
            services.AddSingleton<ResultWindowVm>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            await Host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            await Host.StopAsync().ConfigureAwait(false);
            _host = null;
        }
    }
}
