using LabelPrinter.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabelPrinter
{
    /// <summary>
    /// Interaction logic for PrintJobWindow.xaml
    /// </summary>
    public partial class PrintJobWindow : Window
    {
        PrintJobsViewModel main;
        public PrintJobWindow()
        {
            main = new PrintJobsViewModel();
            DataContext = main;
            InitializeComponent();
        }         
    }
}
