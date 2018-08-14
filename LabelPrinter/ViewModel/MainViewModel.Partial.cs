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
                RaisePropertyChanged(nameof(BitmapImage));
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
                RaisePropertyChanged(nameof(HowManyCoppies));
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
        /// <summary>
        /// Used For The Width of Label
        /// </summary>
        public int LabelWidth
        {
            get { return labelWidth; }
            set
            {
                if (value != labelWidth)
                {
                    labelWidth = value;
                    RaisePropertyChanged(nameof(LabelWidth));
                }
            }
        }
        int labelHeight = 435;
        /// <summary>
        /// used for the height of Label
        /// </summary>
        public int LabelHeight
        {
            get { return labelHeight; }
            set
            {
                if (value != labelHeight)
                {
                    labelHeight = value;
                    RaisePropertyChanged(nameof(LabelHeight));
                }
            }
        }

        int _distanceFromLeft;
        /// <summary>
        /// Distance from left for the Label
        /// </summary>
        public int DistanceFromLeft
        {
            get { return _distanceFromLeft; }
            set
            {
                _distanceFromLeft = value;
                RaisePropertyChanged(nameof(DistanceFromLeft));
            }
        }

        int _codeSize;
        /// <summary>
        /// Used for Barcode Width
        /// </summary>
        public int CodeSize
        {
            get { return _codeSize; }
            set
            {
                _codeSize = value;
                RaisePropertyChanged(nameof(CodeSize));
            }
        }

        int _heightOfCode;
        /// <summary>
        /// Used for Barcode Height
        /// </summary>
        public int HeightOfCode
        {
            get { return _heightOfCode; }
            set
            {
                _heightOfCode = value;
                RaisePropertyChanged(nameof(HeightOfCode));
            }
        }
        /// <summary>
        /// List of all BarCodes
        /// </summary>
        public List<string> BarCode { get; set; } = new List<string> { "2/5 Interleaved", "Code128", "Code39", "DataMatrix", "EAN13", "EAN8" };

        string _selectedBarCode;
        /// <summary>
        /// Selected Barcode From  the Combobox
        /// </summary>
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

            Row1.SelectedCharWidth = Row1.CharWidths.FirstOrDefault();
            Row2.SelectedCharWidth = Row2.CharWidths.FirstOrDefault();
            Row3.SelectedCharWidth = Row3.CharWidths.FirstOrDefault();
            Row4.SelectedCharWidth = Row4.CharWidths.FirstOrDefault();
            Row5.SelectedCharWidth = Row5.CharWidths.FirstOrDefault();
            Row6.SelectedCharWidth = Row6.CharWidths.FirstOrDefault();
            Row7.SelectedCharWidth = Row7.CharWidths.FirstOrDefault();
            Row8.SelectedCharWidth = Row8.CharWidths.FirstOrDefault();
            Row9.SelectedCharWidth = Row9.CharWidths.FirstOrDefault();
            Row10.SelectedCharWidth = Row10.CharWidths.FirstOrDefault();
            Row11.SelectedCharWidth = Row11.CharWidths.FirstOrDefault();
            Row12.SelectedCharWidth = Row12.CharWidths.FirstOrDefault();
            Row13.SelectedCharWidth = Row13.CharWidths.FirstOrDefault();
            Row14.SelectedCharWidth = Row14.CharWidths.FirstOrDefault();
            Row15.SelectedCharWidth = Row15.CharWidths.FirstOrDefault();

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
