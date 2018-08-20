using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelPrinter.ViewModel
{
    public class SetUpViewModel : ViewModelBase
    {
        public List<string> ScalesModel { get; set; } = new List<string> { "None", "METTLER-TOLEDO", "DINI ARGEO DFW-DFWK", "ESSAE SI-810" };
        private string _selectedScalesModel;
       
        public string SelectedScalesModel
        {
            get { return _selectedScalesModel; }
            set
            {
                _selectedScalesModel = value;
                RaisePropertyChanged("SelectedScalesModel");
            }
        }

        public List<string> ScalesPort { get; set; } = new List<string> { "None", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10", "COM11", "COM12", "COM13", "COM14", "COM15"};
        private string _selectedScalesPort;

        public string SelectedScalesPort
        {
            get { return _selectedScalesPort; }
            set
            {
                _selectedScalesModel = value;
                RaisePropertyChanged("SelectedScalesPort");
            }
        }

        public List<string> Printer { get; set; } = new List<string> { "None", "FAX", "Microsoft Print to PDF", "Microsoft XPS Document Writer", "Nitro PDF Creator (Pro 10)", "Send To OneNote 2013" };
        private string _selectedPrinter;

        public string SelectedPrinter
        {
            get { return _selectedPrinter; }
            set
            {
                _selectedPrinter = value;
                RaisePropertyChanged("SelectedPrinter");
            }
        }

        string _text1;

        public string BlackLineText
        {
            get { return _text1; }
            set {
                _text1 = value;
                RaisePropertyChanged(nameof(BlackLineText));
            }
        }

        public SetUpViewModel()
        {
            SelectedScalesModel = "ESSAE SI-810";
            
        }
    }
}
