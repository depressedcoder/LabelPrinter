using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace LabelPrinter.Model
{
    public class Label : ViewModelBase
    {
        List<Row> _rows;

        public List<Row> Rows
        {
            get => _rows;
            set
            {
                _rows = value;
                RaisePropertyChanged(nameof(Rows));
            }
        }

        int _howManyCopies = 1;
        /// <summary>
        /// How many copies will be printed
        /// </summary>
        public int HowManyCoppies
        {
            get => _howManyCopies;
            set
            {
                _howManyCopies = value;
                RaisePropertyChanged(nameof(HowManyCoppies));
            }
        }

        bool _isAutomaticCuttingDevice;

        public bool IsAutomaticCuttingDevice
        {
            get => _isAutomaticCuttingDevice;
            set
            {
                _isAutomaticCuttingDevice = value;
                RaisePropertyChanged(nameof(IsAutomaticCuttingDevice));
            }
        }
        int _labelWidth = 315;
        /// <summary>
        /// Used For The Width of Label
        /// </summary>
        public int LabelWidth
        {
            get => _labelWidth;
            set
            {
                if (value != _labelWidth)
                {
                    _labelWidth = value;
                    RaisePropertyChanged(nameof(LabelWidth));
                }
            }
        }
        int _labelHeight = 423;
        /// <summary>
        /// used for the height of Label
        /// </summary>

        public int LabelHeight
        {
            get => _labelHeight;
            set
            {
                if (value != _labelHeight)
                {
                    _labelHeight = value;
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
            get => _distanceFromLeft;
            set
            {
                _distanceFromLeft = value;
                RaisePropertyChanged(nameof(DistanceFromLeft));
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

        string _selectedLabelName;
        /// <summary>
        /// Selected Label Name From ComboBox
        /// </summary>
        public string SelectedLabelName
        {
            get => _selectedLabelName;
            set
            {
                _selectedLabelName = value;
                RaisePropertyChanged(nameof(SelectedLabelName));
            }
        }

        Barcode _barcode;
        public Barcode Barcode
        {
            get => _barcode;
            set { _barcode = value; RaisePropertyChanged(nameof(Barcode)); }
        }
    }
}