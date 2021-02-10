using ComponentsApp.UI.ViewModels;
using MaterialDesignExtensions.Controls;

namespace ComponentsApp.UI.Views
{
    /// <summary>
    /// Логика взаимодействия для ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : MaterialWindow
    {
        public ResultWindow()
        {
            InitializeComponent();

            DataContext = new ResultWindowVm();
        }
    }
}
