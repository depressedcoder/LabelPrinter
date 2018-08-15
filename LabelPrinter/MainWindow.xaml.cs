using LabelPrinter.ViewModel;
using System.Windows;

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
            DataContext = main;
        }

        void UpdateLabelEvent(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;

            if (vm != null)
            {
                vm.PreviewLabel();
            }
        }
    }
}
