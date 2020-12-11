using ComponentsApp.WPF.ViewModels;
using MaterialDesignExtensions.Controls;

namespace ComponentsApp.WPF.Views
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
