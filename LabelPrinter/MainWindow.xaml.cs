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
            InitializeComponent();
            main = new MainViewModel();
            this.DataContext = main;
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();
            System.Drawing.Image img = b.Encode(BarcodeLib.TYPE.UPCA, "038000356216", System.Drawing.Color.Black, System.Drawing.Color.White, 290, 120);
            
        }

        private void UpdateLabelEvent(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MainViewModel;

            if (vm != null)
            {
                vm.UpdateLabelCommand.Execute(null);
            }
        }
        
    }
}
