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

        public static IHostBuilder CreateHostBuilder(string[] args)=>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices(App.ConfigureServices);       
    }
}
