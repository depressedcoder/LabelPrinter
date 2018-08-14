using GalaSoft.MvvmLight.Command;
using LabelPrinter.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel
    {
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

        BitmapImage _bitmapImage;

        public BitmapImage BitmapImage
        {
            get
            {
                return _bitmapImage;
            }
            set
            {
                _bitmapImage = value;
                RaisePropertyChanged("BitmapImage");
            }
        }

        int _howManyCopies;

        /// <summary>
        /// How many copies will be printed
        /// </summary>
        public int HowManyCoppies
        {
            get { return _howManyCopies; }
            set
            {
                _howManyCopies = value;
                RaisePropertyChanged("HowManyCoppies");
            }
        }

        bool _isAutomaticCuttingDevice;
        public bool IsAutomaticCuttingDevice
        {
            get { return _isAutomaticCuttingDevice; }
            set
            {
                _isAutomaticCuttingDevice = value;
                RaisePropertyChanged("IsAutomaticCuttingDevice");
            }
        }
        int labelWidth = 315;
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
        int labelHeight = 435;
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

        int _distanceFromLeft;
        public int DistanceFromLeft
        {
            get { return _distanceFromLeft; }
            set
            {
                _distanceFromLeft = value;
                RaisePropertyChanged("DistanceFromLeft");
            }
        }

        int _codeSize;

        public int CodeSize
        {
            get { return _codeSize; }
            set
            {
                _codeSize = value;
                RaisePropertyChanged("CodeSize");
            }
        }

        int _heightOfCode;
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
        string _selectedBarCode;

        public string SelectedBarCode
        {
            get { return _selectedBarCode; }
            set
            {
                _selectedBarCode = value;
                RaisePropertyChanged("SelectedBarCode");
                PreviewLabel();
            }
        }

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

            Row1.SelectedCharWidth = 8;
            Row2.SelectedCharWidth = 8;
            Row3.SelectedCharWidth = 8;
            Row4.SelectedCharWidth = 8;
            Row5.SelectedCharWidth = 8;
            Row6.SelectedCharWidth = 8;
            Row7.SelectedCharWidth = 8;
            Row8.SelectedCharWidth = 8;
            Row9.SelectedCharWidth = 8;
            Row10.SelectedCharWidth = 8;
            Row11.SelectedCharWidth = 8;
            Row12.SelectedCharWidth = 8;
            Row13.SelectedCharWidth = 8;
            Row14.SelectedCharWidth = 8;
            Row15.SelectedCharWidth = 8;

            CodeSize = 2;
            HeightOfCode = 5;

            SelectedBarCode = BarCode.FirstOrDefault();
            
            SaveButtonCommand = new RelayCommand(SaveCommand);
            NewButtonCommand = new RelayCommand(NewCommand);
            SetUpButtonCommand = new RelayCommand(SetUpCommand);
            PrintButtonCommand = new RelayCommand(PrintCommand);
            PrintJobsButtonCommand = new RelayCommand(PrintJobsCommand);
            ExitButtonCommand = new RelayCommand(ExitCommand);
            UpdateLabelCommand = new RelayCommand(UpdateLabel);
        }
    }
}
