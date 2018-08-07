using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Packaging;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using Bytescout.BarCode;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LabelPrinter.Model;
using Microsoft.Win32;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LabelPrinter.ViewModel
{

    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string value;
        public string Value
        {
            get { return value; }
            set
            {
                this.value = value; 
                RaisePropertyChanged("Value");
            }
        }

        private SymbologyType symbology;

        public SymbologyType Symbology
        {
            get { return symbology; }
            set
            {
                symbology = value;
                RaisePropertyChanged("Symbology");
            }
        }

        private int _howManyCopies;
        public int HowManyCoppies
        {
            get { return _howManyCopies; }
            set
            {
                _howManyCopies = value; 
                RaisePropertyChanged("HowManyCoppies");
            }
        }

        private bool _isAutomaticCuttingDevice;
        public bool IsAutomaticCuttingDevice
        {
            get { return _isAutomaticCuttingDevice; }
            set
            {
                _isAutomaticCuttingDevice = value;
                RaisePropertyChanged("IsAutomaticCuttingDevice");
            }
        }

        //public int LabelWidth { get; set; } = 305;
        private int labelWidth = 315;
        public int LabelWidth
        {
            get { return labelWidth; }
            set
            {
                if (value != labelWidth)
                {
                    labelWidth = value;
                    RaisePropertyChanged("LabelWidth");
                }
            }
        }
        //public int LabelHeight { get; set; } = 200;
        private int labelHeight = 435;
        public int LabelHeight
        {
            get { return labelHeight; }
            set
            {
                if (value != labelHeight)
                {
                    labelHeight = value;
                    RaisePropertyChanged("LabelHeight");
                }
            }
        }

        private int _distanceFromLeft;
        public int DistanceFromLeft
        {
            get { return _distanceFromLeft; }
            set
            {
                _distanceFromLeft = value;
                RaisePropertyChanged("DistanceFromLeft");
            }
        }

        private int _codeSize ;

        public int CodeSize
        {
            get { return _codeSize; }
            set
            {
                _codeSize = value;
                RaisePropertyChanged("CodeSize");
            }
        }

        private int _heightOfCode;
        public int HeightOfCode
        {
            get { return _heightOfCode; }
            set
            {
                _heightOfCode = value;
                RaisePropertyChanged("HeightOfCode");
            }
        }

        public List<string> BarCode { get; set; } = new List<string> { "2/5 Interleaved", "Code128", "Code39", "DataMatrix", "EAN13", "EAN8" };
        private string _selectedBarCode;

        public string SelectedBarCode
        {
            get { return _selectedBarCode; }
            set
            {
                _selectedBarCode = value;
                RaisePropertyChanged("SelectedBarCode");
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Row1 = new LabelRow();
            Row2 = new LabelRow();
            Row3 = new LabelRow();
            Row4 = new LabelRow();
            Row5 = new LabelRow();
            Row6 = new LabelRow();
            Row7 = new LabelRow();
            Row8 = new LabelRow();
            Row9 = new LabelRow();
            Row10 = new LabelRow();
            Row11 = new LabelRow();
            Row12 = new LabelRow();
            Row13 = new LabelRow();
            Row14 = new LabelRow();
            Row15 = new LabelRow();

            Row1.Text = "NORSEL AG77";
            Row2.Text = "<BAR120021++>";
            Row3.Text = "120001++";
            Row4.Text = "'''";
            Row5.Text = "jhjhj";
            Row6.Text = "jh";
            Row7.Text = "jh";
            Row8.Text = "jhjh";
            Row9.Text = "jhjh";
            Row10.Text = "jhjh";
            Row11.Text = "jhjj";
            Row12.Text = "jh";
            Row13.Text = "kj";
            Row14.Text = "jhj";
            Row15.Text = "hj";

            Row1.SelectedCharWidth = 15;
            Row2.SelectedCharWidth = 15;
            Row3.SelectedCharWidth = 15;
            Row4.SelectedCharWidth = 15;
            Row5.SelectedCharWidth = 18;
            Row6.SelectedCharWidth = 18;
            Row7.SelectedCharWidth = 18;
            Row8.SelectedCharWidth = 15;
            Row9.SelectedCharWidth = 15;
            Row10.SelectedCharWidth = 15;
            Row11.SelectedCharWidth = 18;
            Row12.SelectedCharWidth = 15;
            Row13.SelectedCharWidth = 18;
            Row14.SelectedCharWidth = 15;
            Row15.SelectedCharWidth = 18;

           
            /*Value = "12345";
            Symbology = SymbologyType.Code128;*/

            SaveButtonCommand = new RelayCommand(SaveCommand);
            NewButtonCommand = new RelayCommand(NewCommand);
            SetUpButtonCommand = new RelayCommand(SetUpCommand);
            PrintButtonCommand = new RelayCommand(PrintCommand);
            PrintJobsButtonCommand = new RelayCommand(PrintJobsCommand);
            ExitButtonCommand = new RelayCommand(ExitCommand);
            UpdateLabelCommand = new RelayCommand(UpdateLabel);
        }

        private void UpdateLabel()
        {
            
        }

        private void ExitCommand()
        {
            //Exit Button
        }

        private void PrintJobsCommand()
        {
            //PrintJobs Button
        }

        private void PrintCommand()
        {
            //Print Button
        }

        private void SetUpCommand()
        {
            //Set Up Button
        }

        private void NewCommand()
        {
            //New Button
        }
        string filepath;
        private void SaveCommand()
        {
            //OpenFileDialog open = new OpenFileDialog();
            //open.Multiselect = false;
            //open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            //bool? result = open.ShowDialog();

            //if (result == true)
            //{
            //    filepath = open.FileName; // Stores Original Path in Textbox    
            //    ImageSource imgsource = new BitmapImage(new Uri(filepath)); // Just show The File In Image when we browse It
            //    Clientimg.Source = imgsource;
            //}
        }

        public LabelRow Row1 { get; set; }
        public LabelRow Row2 { get; set; }
        public LabelRow Row3 { get; set; }
        public LabelRow Row4 { get; set; }
        public LabelRow Row5 { get; set; }
        public LabelRow Row6 { get; set; }
        public LabelRow Row7 { get; set; }
        public LabelRow Row8 { get; set; }
        public LabelRow Row9 { get; set; }
        public LabelRow Row10 { get; set; }
        public LabelRow Row11 { get; set; }
        public LabelRow Row12 { get; set; }
        public LabelRow Row13 { get; set; }
        public LabelRow Row14 { get; set; }
        public LabelRow Row15 { get; set; }
        public RelayCommand SaveButtonCommand { get; private set; }
        public RelayCommand NewButtonCommand { get; private set; }
        public RelayCommand SetUpButtonCommand { get; private set; }
        public RelayCommand PrintButtonCommand { get; private set; }
        public RelayCommand PrintJobsButtonCommand { get; private set; }
        public RelayCommand ExitButtonCommand { get; private set; }
        public RelayCommand UpdateLabelCommand { get; private set; }
    }
}