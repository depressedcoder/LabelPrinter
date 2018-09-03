﻿using GalaSoft.MvvmLight;

namespace LabelPrinter.Model
{
    public class Row: ViewModelBase
    {
        string _text;
        /// <summary>
        /// Used for all the 15 text inputs
        /// </summary>
        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                RaisePropertyChanged(nameof(Text));
            }
        }
        
        int _selectedCharWidth;
        /// <summary>
        /// Used For Selected Char Width
        /// </summary>
        public int SelectedCharWidth
        {
            get => _selectedCharWidth;
            set
            {
                _selectedCharWidth = value;
                if (IsHigh)
                {
                    CharWidth = value*2;
                }
                else
                {
                    CharWidth = value;
                }
                RaisePropertyChanged(nameof(SelectedCharWidth));
            }
        }

        bool _isHigh;
        /// <summary>
        /// the text input font size is doubled with the selected char Width When its Checked 
        /// </summary>
        public bool IsHigh
        {
            get => _isHigh;
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
                RaisePropertyChanged(nameof(IsHigh));
            }
        }

        bool _isBold;
        /// <summary>
        /// Text input is Bold When Checked
        /// </summary>
        public bool IsBold
        {
            get => _isBold;
            set
            {
                _isBold = value;
                RaisePropertyChanged(nameof(IsBold));
            }
        }

        bool _isUnderLined;
        /// <summary>
        /// Text input is Underlined When Checked 
        /// </summary>
        public bool IsUnderlined
        {
            get => _isUnderLined;
            set
            {
                _isUnderLined = value;
                RaisePropertyChanged(nameof(IsUnderlined));
            }
        }

        int _charWidth;

        /// <summary>
        /// Used for the Char Width
        /// </summary>
        public int CharWidth
        {
            get => _charWidth;

            set
            {
                _charWidth = value;

                RaisePropertyChanged(nameof(CharWidth));
            }
        }

    }
}