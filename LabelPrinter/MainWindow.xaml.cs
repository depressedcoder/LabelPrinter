using LabelPrinter.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabelPrinter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainViewModel main;
        public MainWindow()
        {
            main = new MainViewModel();
            this.DataContext = main;
        }

        private void UpdateLabelEvent(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;

            if (vm != null)
            {
                vm.PreviewLabel();
            }
        }

        private void TextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var s = sender as TextBox;
            // Use SelectionStart property to find the caret position.
            // Insert the previewed text into the existing text in the textbox.
            var text = s.Text.Insert(s.SelectionStart, e.Text);

            double d;
            // If parsing is successful, set Handled to false
            e.Handled = !double.TryParse(text, out d);
        }

    }
}
