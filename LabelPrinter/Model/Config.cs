using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelPrinter.Model
{
    public class Config : ViewModelBase
    {

        private string _selectedScalesModel;

        public string SelectedScalesModel
        {
            get { return _selectedScalesModel; }
            set
            {
                _selectedScalesModel = value;
            }
        }

        private string _selectedScalesPort;

        public string SelectedScalesPort
        {
            get { return _selectedScalesPort; }
            set
            {
                _selectedScalesPort = value;
            }
        }

        private string _selectedPrinter;

        public string SelectedPrinter
        {
            get { return _selectedPrinter; }
            set
            {
                _selectedPrinter = value;
            }
        }
        
        private string _selectedPrinterport;

        public string SelectedPrinterPort
        {
            get { return _selectedPrinterport; }
            set
            {
                _selectedPrinterport = value;
            }
        }

        private string _selectedDateConnection;

        public string SelectedDataConnection
        {
            get { return _selectedDateConnection; }
            set
            {
                _selectedDateConnection = value;

            }
        }
        
        private int _density;

        public int Density
        {
            get { return _density; }
            set
            {
                _density = value;

            }
        }
        
        private int _speed;

        public int Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;
            }
        }

        string _text1;
        public string BlackLineText
        {
            get { return _text1; }
            set { _text1 = value; }
        }
        
        string _text2;
        public string GapControlText
        {
            get { return _text2; }
            set { _text2 = value; }
        }
        
        private string _locationOfFile;

        public string LocationOfFile
        {
            get { return _locationOfFile; }
            set { _locationOfFile = value; }
        }
        
        private string _odbcConnection;
        public string ODBCConnectionString
        {
            get { return _odbcConnection; }
            set { _odbcConnection = value; }
        }
        
        private bool _isCreateOrExport;
        public bool IsCreateOrExport
        {
            get { return _isCreateOrExport; }
            set{ _isCreateOrExport = value; }
        }
        
        private int _radioButtonValue = 0;
        public int RadioButtonValue
        {
            get { return _radioButtonValue; }
            set { _radioButtonValue = value; }
        }
    }
}
