using MaterialDesignExtensions.Controls;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ComponentsApp.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для InputWindow.xaml
    /// </summary>
    public partial class InputWindow : MaterialWindow
    {
        public InputWindow()
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
                // MoveFocus takes a TraveralReqest as its argument.
                var request = new TraversalRequest(FocusNavigationDirection.Next);
                ((UIElement)sender).MoveFocus(request);
            }


            //// Gets the element with keyboard focus.
            //UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;

            //// Change keyboard focus.
            //if (elementWithFocus != null)
            //{
            //    if (elementWithFocus.MoveFocus(request)) e.Handled = true;
            //}

        }
    }
}
