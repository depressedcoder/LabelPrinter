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
            e.Handled = !IsValid(((TextBox)sender).Text + e.Text);
        }
        public static bool IsValid(string str)
        {
            int i;
            return int.TryParse(str, out i) && i >= 1 && i <= 600;
        }

    }
}
