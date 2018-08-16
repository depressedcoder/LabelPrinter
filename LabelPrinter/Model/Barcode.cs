using GalaSoft.MvvmLight;

namespace LabelPrinter.Model
{
    public class Barcode: ViewModelBase
    {
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
    }
}
