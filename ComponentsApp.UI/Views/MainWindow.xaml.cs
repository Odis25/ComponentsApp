using MaterialDesignExtensions.Controls;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ComponentsApp.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MaterialWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainTopPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new AboutWindow();
            window.ShowDialog();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = (TextBox)sender;
            Regex regex;

            if (tb.Text.Contains("."))
            {
                if (tb.SelectedText.Length > 0 && tb.SelectedText.Contains("."))
                {
                    regex = new Regex("[^0-9.]+");
                }
                else
                {
                    regex = new Regex("[^0-9]+");
                }
                e.Handled = regex.IsMatch(e.Text);
            }
            else
            {
                regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var request = new TraversalRequest(FocusNavigationDirection.Next);

                ((UIElement)sender).MoveFocus(request);

                var element = Keyboard.FocusedElement as TextBox;

                if (element != null)
                {
                    element.SelectAll();
                }
            }
        }
    }
}
