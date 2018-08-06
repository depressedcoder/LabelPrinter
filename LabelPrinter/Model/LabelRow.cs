using System.Collections.Generic;
using GalaSoft.MvvmLight;

namespace LabelPrinter.Model
{
    public class LabelRow: ViewModelBase
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RaisePropertyChanged("Text");
            }
        }

        public List<int> CharWidths { get; set; } = new List<int> { 6, 8, 10, 12, 15, 18, 20, 24 };

        private int _selectedCharWidth;

        public int SelectedCharWidth
        {
            get { return _selectedCharWidth; }
            set
            {
                _selectedCharWidth = value;
                RaisePropertyChanged("SelectedCharWidth");

                if (IsHigh)
                {
                    CharWidth = value*2;
                }
                else
                {
                    CharWidth = value;
                }
            }
        }

        private bool _isHigh;

        public bool IsHigh
        {
            get { return _isHigh; }
            set
            {
                _isHigh = value;
                if (_isHigh)
                {
                    CharWidth = SelectedCharWidth*2;
                }
                else
                {
                    CharWidth = CharWidth/2;
                }
                RaisePropertyChanged("IsHigh");
            }
        }

        private bool _isBold;

        public bool IsBold
        {
            get { return _isBold; }
            set
            {
                _isBold = value;
                RaisePropertyChanged("IsBold");
            }
        }

        private bool _isUnderLined;

        public bool IsUnderlined
        {
            get { return _isUnderLined; }
            set
            {
                _isUnderLined = value;
                RaisePropertyChanged("IsUnderlined");
            }
        }

        private int _charWidth;

        public int CharWidth
        {
            get
            {
                return _charWidth;
            }

            set
            {
                _charWidth = value;

                RaisePropertyChanged("CharWidth");
            }
        }

    }
}
