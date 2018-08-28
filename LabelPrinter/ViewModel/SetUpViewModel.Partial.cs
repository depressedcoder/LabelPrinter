using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelPrinter.ViewModel
{
    public partial class SetUpViewModel
    {
        /// <summary>
        /// For Scales model combobox
        /// </summary>
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

        /// <summary>
        /// For Scales Port Combobox
        /// </summary>
        public List<string> ScalesPort { get; set; } = new List<string> { "None", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9", "COM10", "COM11", "COM12", "COM13", "COM14", "COM15" };
        private string _selectedScalesPort;

        public string SelectedScalesPort
        {
            get { return _selectedScalesPort; }
            set
            {
                _selectedScalesPort = value;
                RaisePropertyChanged("SelectedScalesPort");
            }
        }
        /// <summary>
        /// For Printer Combobox
        /// </summary>
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
        /// <summary>
        /// For printer Port Combobox
        /// </summary>
        public List<string> PrinterPort { get; set; } = new List<string> { "USB", "LPT1", "LPT2", "LPT3" };
        private string _selectedPrinterport;

        public string SelectedPrinterPort
        {
            get { return _selectedPrinterport; }
            set
            {
                _selectedPrinterport = value;
                RaisePropertyChanged("SelectedPrinterPort");
            }
        }
        /// <summary>
        /// For Date Connection ComboBox
        /// </summary>
        public List<string> DataConnection { get; set; } = new List<string> { "None", "Text Files", "Data Base Oracle", "Data Base MySQL", "Data Base MySQL Server" };
        private string _selectedDateConnection;

        public string SelectedDataConnection
        {
            get { return _selectedDateConnection; }
            set
            {
                _selectedDateConnection = value;
                RaisePropertyChanged("DateConnection");
            }
        }
        /// <summary>
        /// Slider Value for Density
        /// </summary>
        private int _density;

        public int Density
        {
            get { return _density; }
            set
            {
                _density = value;
                RaisePropertyChanged(nameof(Density));
            }
        }
        /// <summary>
        /// Slider Value for Speed
        /// </summary>
        private int _speed;

        public int Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
                RaisePropertyChanged(nameof(Speed));
            }
        }
        /// <summary>
        /// For Textbox under the Blackline Text Radio Button
        /// </summary>
        string _text1;

        public string BlackLineText
        {
            get { return _text1; }
            set
            {
                _text1 = value;
                RaisePropertyChanged(nameof(BlackLineText));
            }
        }
        /// <summary>
        /// For TextBox under the GapControl Text Radio Button
        /// </summary>
        string _text2;

        public string GapControlText
        {
            get { return _text2; }
            set
            {
                _text2 = value;
                RaisePropertyChanged(nameof(GapControlText));
            }
        }
        /// <summary>
        /// For location of file textbox
        /// </summary>
        private string _locationOfFile;

        public string LocationOfFile
        {
            get { return _locationOfFile; }
            set
            {
                _locationOfFile = value;
                RaisePropertyChanged(nameof(LocationOfFile));
            }
        }
        /// <summary>
        /// For ODBC Connection String Textbox
        /// </summary>
        private string _odbcConnection;

        public string ODBCConnectionString
        {
            get { return _odbcConnection; }
            set
            {
                _odbcConnection = value;
                RaisePropertyChanged(nameof(ODBCConnectionString));
            }
        }
        /// <summary>
        /// For Create or Export Textbox
        /// </summary>
        private bool _isCreateOrExport;

        public bool IsCreateOrExport
        {
            get { return _isCreateOrExport; }
            set
            {
                _isCreateOrExport = value;
                RaisePropertyChanged(nameof(IsCreateOrExport));
            }
        }
        /// <summary>
        /// Three Radio buttons Value presented in int value
        /// </summary>
        private int _radioButtonValue = 0;
        public int RadioButtonValue
        {
            get{ return _radioButtonValue; }
            set
            {
                _radioButtonValue = value;
                RaisePropertyChanged(nameof(RadioButtonValue));
            }
        }

        public RelayCommand SaveButtonCommand { get; private set; }
        public RelayCommand ExitButtonCommand { get; private set; }
        public RelayCommand ChangeButtonCommand { get; private set; }
        public RelayCommand TestConnectionButtonCommand { get; private set; }

        public SetUpViewModel()
        {
            SelectedScalesModel = "ESSAE SI-810";
            SelectedScalesPort = "None";
            SelectedDataConnection = "None";

            SaveButtonCommand = new RelayCommand(SaveCommand);
            ExitButtonCommand = new RelayCommand(ExitCommand);
            ChangeButtonCommand = new RelayCommand(ChangeCommand);
            TestConnectionButtonCommand = new RelayCommand(TestConnectionCommand);
        }
        
    }
}
