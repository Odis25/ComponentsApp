using Microsoft.Extensions.DependencyInjection;

namespace ComponentsApp.UI.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowVm MainWindowVm => App.Host.Services.GetRequiredService<MainWindowVm>();
        public ResultWindowVm ResultWindowVm => App.Host.Services.GetRequiredService<ResultWindowVm>();
    }
}
