﻿using GalaSoft.MvvmLight.Command;
using LabelPrinter.Model;
using LabelPrinter.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

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
        public List<string> Printer { get; set; } = GodexPrinter.GetDriverPrinter().ToList(); // new List<string> { "None", "FAX", "Microsoft Print to PDF", "Microsoft XPS Document Writer", "Nitro PDF Creator (Pro 10)", "Send To OneNote 2013" };

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
        public List<string> DataConnection { get; set; } = new List<string> {
                                                                EnumsConverter.GetDescription(DataConnections.TextFile),
                                                                EnumsConverter.GetDescription(DataConnections.Oracle),
                                                                EnumsConverter.GetDescription(DataConnections.MySQL),
                                                                EnumsConverter.GetDescription(DataConnections.MSSQL)
                                                            };
        private string _selectedDateConnection;

        public string SelectedDataConnection
        {
            get { return _selectedDateConnection; }
            set
            {
                _selectedDateConnection = value;
                RaisePropertyChanged(nameof(SelectedDataConnection));
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
        /// For Test Connection Visibility Convertion
        /// </summary>
        private bool _isVisibleForConnection;

        public bool IsVisibleForConnection
        {
            get { return _isVisibleForConnection; }
            set
            {
                _isVisibleForConnection = value;
                RaisePropertyChanged(nameof(IsVisibleForConnection));
            }
        }
        /// <summary>
        /// For Test Connection Visibility Convertion
        /// </summary>
        private bool _isVisibleForLocation;

        public bool IsVisibleForLocation
        {
            get { return _isVisibleForLocation; }
            set
            {
                _isVisibleForLocation = value;
                RaisePropertyChanged(nameof(IsVisibleForLocation));
            }
        }
        private string _egText;

        public string EgText
        {
            get { return _egText; }
            set {
                _egText = value;
                RaisePropertyChanged(nameof(EgText));
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

        public RelayCommand<object> SaveButtonCommand { get; private set; }
        public RelayCommand<object> ExitButtonCommand { get; private set; }
        public RelayCommand ChangeButtonCommand { get; private set; }
        public RelayCommand TestConnectionButtonCommand { get; private set; }

        public SetUpViewModel()
        {
            SetupConfig();

            SaveButtonCommand = new RelayCommand<object>(SaveCommand);
            ExitButtonCommand = new RelayCommand<object>(ExitCommand);
            ChangeButtonCommand = new RelayCommand(ChangeCommand);
            TestConnectionButtonCommand = new RelayCommand(TestConnectionCommand);
        }

        private void SetupConfig()
        {
            Config config = StorageSelector.GetConfig();
            if (config != null)
            { 
                SelectedScalesModel = config.SelectedScalesModel != null ? config.SelectedScalesModel : ScalesModel.FirstOrDefault();
                SelectedScalesPort = config.SelectedScalesPort != null ? config.SelectedScalesPort : ScalesPort.FirstOrDefault();
                SelectedDataConnection = config.SelectedConnection != null ? config.SelectedConnection : DataConnection.FirstOrDefault();
                SelectedPrinter = config.SelectedPrinter;
                SelectedPrinterPort = config.SelectedPrinterPort;
                Density = config.Density;
                Speed = config.Speed;
                RadioButtonValue = config.RadioButtonValue;
                BlackLineText = config.BlackLineText;
                GapControlText = config.GapControlText;
                LocationOfFile = config.LocationOfFile;
                ODBCConnectionString = SelectedDataConnection == EnumsConverter.GetDescription(DataConnections.Oracle) ? config.OracleConnection
                                    : SelectedDataConnection == EnumsConverter.GetDescription(DataConnections.MySQL) ? config.MySqlConnection
                                    : SelectedDataConnection == EnumsConverter.GetDescription(DataConnections.MSSQL) ? config.MssqlConnection
                                    : config.TextConnection;
                IsCreateOrExport = config.IsCreateOrExport;
            }
            else
            {
                SelectedScalesModel = ScalesModel.FirstOrDefault();
                SelectedScalesPort = ScalesPort.FirstOrDefault();
                SelectedDataConnection = DataConnection.FirstOrDefault();
            }
        }
    }
}
