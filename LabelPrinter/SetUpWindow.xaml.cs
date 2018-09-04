using LabelPrinter.ViewModel;
using System.Windows;

namespace LabelPrinter
{
    /// <summary>
    /// Interaction logic for SetUpWindow.xaml
    /// </summary>
    public partial class SetUpWindow : Window
    {
        SetUpViewModel main;

        public SetUpWindow()
        {
            main = new SetUpViewModel();
            DataContext = main;
            InitializeComponent();
        }


        void DataConnectionUpdateEvent(object sender, RoutedEventArgs e)
        {
            if (DataContext is SetUpViewModel vm)
            {
                vm.DataConnectionUpdate();
            }
        }

    }
}
