using ComponentApp.WPF.ViewModels;
using System.Windows;

namespace ComponentApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainWindowVm();
        }      
    }
}
