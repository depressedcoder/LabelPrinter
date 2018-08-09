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
using System.Text.RegularExpressions;

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
        BarcodeHelper _barcodeHelper;
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
        private string _selectedBarCode;

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

        public void PreviewLabel()
        {
            //Add Row 1
            var bitmap = new Bitmap(LabelWidth, labelHeight);


            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);

                //Row 1
                Draw(graphics, Row1.Text, SelectedBarCode, Row1.IsBold, Row1.IsUnderlined, Row1.IsHigh, Row1.SelectedCharWidth, 10f, 10f);
                //Row 2
                Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 100f);
                //Row 3
                Draw(graphics, Row3.Text, SelectedBarCode, Row3.IsBold, Row3.IsUnderlined, Row3.IsHigh, Row3.SelectedCharWidth, 10f, 120f);
                //Row 4
                Draw(graphics, Row4.Text, SelectedBarCode, Row4.IsBold, Row4.IsUnderlined, Row4.IsHigh, Row4.SelectedCharWidth, 10f, 140f);

                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                //Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, 10f);
                
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

        public void Draw(Graphics graphics, string input, string barcode, bool isBold, bool isUnderLine, bool isHigh, int selectedCharwidth, float x, float y)
        {
            var labels = input.Split(' ');

            foreach (var label in labels)
            {
                if (Regex.IsMatch(label, "^<BAR.*>"))
                {
                    var barCodeLabel = Regex.Replace(label, "<BAR|\\++>|>", "");

                    var barcodeImage = _barcodeHelper.GetBarcode(barcode, barCodeLabel, CodeSize, HeightOfCode);
                    graphics.DrawImage(barcodeImage, new PointF(x, y));

                    x = barcodeImage.Width + 5f;
                }
                else if (Regex.IsMatch(label, "^<IMG.*>"))
                {
                    var imageLabel = Regex.Replace(label, "<IMG|>", "");

                    var image = System.Drawing.Image.FromFile($"{imageLabel}.bmp");
                        
                    graphics.DrawImage(image, new PointF(x, y));

                    x = image.Width + 5f;
                }
                else if (!string.IsNullOrEmpty(label))
                {
                    graphics.DrawString(label, GetRowFont(isBold, isUnderLine, isHigh, selectedCharwidth), Brushes.Black, new PointF(x, y));
                }

                x += 5f;
                graphics.DrawString(" ", GetRowFont(isBold, isUnderLine, isHigh, selectedCharwidth), Brushes.Black, new PointF(x, y));

            }
        }

        Font GetRowFont(bool isBold, bool isUnderLine, bool isHigh, int selectedCharwidth)
        {
            FontStyle style = FontStyle.Regular;
            if (isBold) style |= FontStyle.Bold;
            if (isUnderLine) style |= FontStyle.Underline;
            if (isHigh)
            {
                selectedCharwidth = selectedCharwidth * 2;
            }
            var font = new Font("Arial", selectedCharwidth, style | style);

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

            CodeSize = 2;
            HeightOfCode = 2;
            SelectedBarCode = "Code39";


            SaveButtonCommand = new RelayCommand(SaveCommand);
            NewButtonCommand = new RelayCommand(NewCommand);
            SetUpButtonCommand = new RelayCommand(SetUpCommand);
            PrintButtonCommand = new RelayCommand(PrintCommand);
            PrintJobsButtonCommand = new RelayCommand(PrintJobsCommand);
            ExitButtonCommand = new RelayCommand(ExitCommand);
            UpdateLabelCommand = new RelayCommand(UpdateLabel);

            _barcodeHelper = new BarcodeHelper();
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