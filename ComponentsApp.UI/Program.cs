using Microsoft.Extensions.Hosting;
using System;

namespace ComponentsApp.UI
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }

        public static IHost CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(App.ConfigureServices)
                .Build();
        }
    }
}
