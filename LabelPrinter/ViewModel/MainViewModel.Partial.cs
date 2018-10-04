using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LabelPrinter.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using LabelPrinter.Storage;
using System.Data;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        #region global variable

        GodexPrinter _printer;
        readonly StorageSelector _storageSelector;

        #endregion

        #region public relayCommand(s)

        public RelayCommand SaveButtonCommand { get; }
        public RelayCommand NewButtonCommand { get; }
        public RelayCommand SetUpButtonCommand { get; }
        public RelayCommand DeleteButtonCommand { get; }
        public RelayCommand PrintButtonCommand { get; }
        public RelayCommand PrintJobsButtonCommand { get; }
        public RelayCommand ExitButtonCommand { get; }
        public RelayCommand UpdateLabelCommand { get; }

        #endregion

        #region private member(s)

        private readonly DataTable data = new DataTable("PrintingHistory", "LabelPrinter");
        private readonly string tempDir = @"..\TEMP";
        private readonly string templateFile = string.Empty;

        #endregion

        #region public member(s)
        private List<string> _printerTypes;
        public List<string> PrinterTypes
        {
            get
            {
                if(_printerTypes == null)
                {
                    _printerTypes = new List<string>();
                }
                if(_printerTypes.Count < 2)
                {
                    _printerTypes.Add(EnumsConverter.GetDescription(PrinterType.DirectThermal));
                    _printerTypes.Add(EnumsConverter.GetDescription(PrinterType.ThermalTransfer));
                }

               return _printerTypes;
            }
            set
            {
                _printerTypes = value;
                RaisePropertyChanged(nameof(PrinterTypes));
            }
        }

        Label _label;
        public Label Label
        {
            get => _label;
            set { _label = value; RaisePropertyChanged(nameof(Label)); }
        }

        BitmapImage _bitmapImage;
        public BitmapImage BitmapImage
        {
            get => _bitmapImage;
            set
            {
                _bitmapImage = value;
                RaisePropertyChanged(nameof(BitmapImage));
            }
        }

        List<string> _labelSource;
        /// <summary>
        /// List of all LabelNames
        /// </summary>
        public List<string> LabelSource
        {
            get => _labelSource;
            set
            {
                _labelSource = value;
                RaisePropertyChanged(nameof(LabelSource));
            }
        }

        /// <summary>
        /// List of all BarCodes
        /// </summary>
        public List<string> BarCodes { get; set; } = new List<string> { "2/5 Interleaved", "Code128", "Code39", "DataMatrix", "EAN13", "EAN8" };

        /// <summary>
        /// Used for the Char Widths ComboBox
        /// </summary>
        public List<int> CharWidths { get; set; } = new List<int> { 6, 8, 10, 12, 15, 18, 20, 24 };

        public int PreviewHeight { set; get; } = 400;
        public int PreviewWeight { set; get; } = 100;

        #endregion
        
        #region constructor(s)

        public MainViewModel()
        {
            _printer = new GodexPrinter();

            _storageSelector = new StorageSelector();

            SaveButtonCommand = new RelayCommand(SaveCommand);
            NewButtonCommand = new RelayCommand(NewCommand);
            SetUpButtonCommand = new RelayCommand(SetUpCommand);
            DeleteButtonCommand = new RelayCommand(DeleteCommand);
            PrintButtonCommand = new RelayCommand(PrintCommand);
            PrintJobsButtonCommand = new RelayCommand(PrintJobsCommand);
            ExitButtonCommand = new RelayCommand(ExitCommand);
            UpdateLabelCommand = new RelayCommand(UpdateLabel);

            LabelSource = _storageSelector.GetStorage().GetLabelNames();

            Label = new Label
            {
                Barcode = new Barcode
                {
                    CodeSize = 2,
                    HeightOfCode = 5,
                    SelectedBarCode = BarCodes.FirstOrDefault()
                },
                Rows = new List<Row>(),
                SelectedLabelName = LabelSource.FirstOrDefault()
            };

            //Add 15 rows in label
            for (var i = 0; i < 15; i++)
            {
                Label.Rows.Add(new Row
                {
                    SelectedCharWidth = CharWidths.FirstOrDefault()
                });
            }
        }

        #endregion

    }
}
