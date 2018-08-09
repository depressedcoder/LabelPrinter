using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LabelPrinter.Helpers;
using LabelPrinter.Model;
using System.Windows.Controls;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

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

        private int _codeSize;

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
        private string _selectedBarCode = "Code128";

        public string SelectedBarCode
        {
            get { return _selectedBarCode; }
            set
            {
                _selectedBarCode = value;
                RaisePropertyChanged("SelectedBarCode");
                UpdateBarCode();
            }
        }

        public void UpdateBarCode()
        {
            //Add Row 1
            var bitmap = new Bitmap(LabelWidth, labelHeight);
            var barcodeHelper = new BarcodeHelper();

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                
                //row1
                graphics.DrawString(Row1.Text, GetRowFont(Row1.IsBold, Row1.IsUnderlined, Row1.IsHigh, Row1.SelectedCharWidth), Brushes.Black, new PointF(10f, 10f));

                if (SelectedBarCode == "Code39")
                    graphics.DrawImage(barcodeHelper.GetCode39Barcode(Row1.Text), new PointF(90f, 10f));
                else if(SelectedBarCode == "Code128")
                    graphics.DrawImage(barcodeHelper.GetCode128Barcode(Row1.Text), new PointF(90f, 10f));
                else if(SelectedBarCode == "EAN13")
                    graphics.DrawImage(barcodeHelper.GetEAN13Barcode(Row1.Text), new PointF(90f, 10f));
                else if (SelectedBarCode == "EAN8")
                    graphics.DrawImage(barcodeHelper.GetEAN8Barcode(Row1.Text), new PointF(90f, 10f));
                else if (SelectedBarCode == "2/5 Interleaved")
                    graphics.DrawImage(barcodeHelper.GetInterleaved2of5Barcode(Row1.Text), new PointF(90f, 10f));

                
                //row2
                graphics.DrawString(Row2.Text, GetRowFont(Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth), Brushes.Black, new PointF(10f, 100f));
                //row3
                graphics.DrawString(Row3.Text, GetRowFont(Row3.IsBold, Row3.IsUnderlined, Row3.IsHigh, Row3.SelectedCharWidth), Brushes.Black, new PointF(10f, 120f));
                //row4
                graphics.DrawString(Row4.Text, GetRowFont(Row4.IsBold, Row4.IsUnderlined, Row4.IsHigh, Row4.SelectedCharWidth), Brushes.Black, new PointF(10f, 140f));
                //row5
                graphics.DrawString(Row5.Text, GetRowFont(Row5.IsBold, Row5.IsUnderlined, Row5.IsHigh, Row5.SelectedCharWidth), Brushes.Black, new PointF(10f, 160f));
                //row6
                graphics.DrawString(Row6.Text, GetRowFont(Row6.IsBold, Row6.IsUnderlined, Row6.IsHigh, Row6.SelectedCharWidth), Brushes.Black, new PointF(10f, 180f));
                //row7
                graphics.DrawString(Row7.Text, GetRowFont(Row7.IsBold, Row7.IsUnderlined, Row7.IsHigh, Row7.SelectedCharWidth), Brushes.Black, new PointF(10f, 200f));
                //row8
                graphics.DrawString(Row8.Text, GetRowFont(Row8.IsBold, Row8.IsUnderlined, Row8.IsHigh, Row8.SelectedCharWidth), Brushes.Black, new PointF(10f, 220f));
                //row9
                graphics.DrawString(Row9.Text, GetRowFont(Row9.IsBold, Row9.IsUnderlined, Row9.IsHigh, Row9.SelectedCharWidth), Brushes.Black, new PointF(10f, 240f));
                //row10
                graphics.DrawString(Row10.Text, GetRowFont(Row10.IsBold, Row10.IsUnderlined, Row10.IsHigh, Row10.SelectedCharWidth), Brushes.Black, new PointF(10f, 260f));
                //row11
                graphics.DrawString(Row11.Text, GetRowFont(Row11.IsBold, Row11.IsUnderlined, Row11.IsHigh, Row11.SelectedCharWidth), Brushes.Black, new PointF(10f, 280f));
                //row12
                graphics.DrawString(Row12.Text, GetRowFont(Row12.IsBold, Row12.IsUnderlined, Row12.IsHigh, Row12.SelectedCharWidth), Brushes.Black, new PointF(10f, 300f));
                //row13
                graphics.DrawString(Row13.Text, GetRowFont(Row13.IsBold, Row13.IsUnderlined, Row13.IsHigh, Row13.SelectedCharWidth), Brushes.Black, new PointF(10f, 320f));
                //row14
                graphics.DrawString(Row14.Text, GetRowFont(Row14.IsBold, Row14.IsUnderlined, Row14.IsHigh, Row14.SelectedCharWidth), Brushes.Black, new PointF(10f, 340f));
                //row15
                graphics.DrawString(Row15.Text, GetRowFont(Row15.IsBold, Row15.IsUnderlined, Row15.IsHigh, Row15.SelectedCharWidth), Brushes.Black, new PointF(10f, 360f));

            }

            using (var ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Bmp);
                ms.Seek(0, SeekOrigin.Begin);

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = ms;
                bitmapImage.EndInit();

                BitmapImage = bitmapImage;
            }
        }

        Font GetRowFont(bool isBold, bool isUnderLine,bool isHigh,int selectedCharwidth)
        {
            FontStyle style = FontStyle.Regular;
            if (isBold) style |= FontStyle.Bold;
            if (isUnderLine) style |= FontStyle.Underline;
            if(isHigh)
            {
                selectedCharwidth = selectedCharwidth * 2;
            }
            var font = new Font("Arial", selectedCharwidth , style|style);

            return font;
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

            Row1.Text = "123";
            Row2.Text = "123";
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

            CodeSize = 50;
            SelectedBarCode = "Code39";

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
        private void SaveCommand()
        {

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