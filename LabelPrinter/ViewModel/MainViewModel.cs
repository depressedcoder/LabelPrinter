using System.Collections.Generic;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LabelPrinter.Helpers;
using LabelPrinter.Model;
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
                var row1Height = Draw(graphics, Row1.Text, SelectedBarCode, Row1.IsBold, Row1.IsUnderlined, Row1.IsHigh, Row1.SelectedCharWidth, 10f, 10f);
                //Row 2
                var row2Height = Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, row1Height);
                //Row 3
                var row3Height = Draw(graphics, Row3.Text, SelectedBarCode, Row3.IsBold, Row3.IsUnderlined, Row3.IsHigh, Row3.SelectedCharWidth, 10f, row2Height + row1Height);
                //Row 4
                var row4Height = Draw(graphics, Row4.Text, SelectedBarCode, Row4.IsBold, Row4.IsUnderlined, Row4.IsHigh, Row4.SelectedCharWidth, 10f, row3Height + row2Height + row1Height);
                //Row5
                var row5Height = Draw(graphics, Row5.Text, SelectedBarCode, Row5.IsBold, Row5.IsUnderlined, Row5.IsHigh, Row5.SelectedCharWidth, 10f, row4Height + row3Height + row2Height + row1Height);
                //ROW6
                var row6Height = Draw(graphics, Row6.Text, SelectedBarCode, Row6.IsBold, Row6.IsUnderlined, Row6.IsHigh, Row6.SelectedCharWidth, 10f, row5Height + row4Height + row3Height + row2Height + row1Height);
                //ROW7
                var row7Height = Draw(graphics, Row7.Text, SelectedBarCode, Row7.IsBold, Row7.IsUnderlined, Row7.IsHigh, Row7.SelectedCharWidth, 10f, row6Height + row5Height + row4Height + row3Height + row2Height + row1Height);
                //ROW8
                var row8Height = Draw(graphics, Row8.Text, SelectedBarCode, Row8.IsBold, Row8.IsUnderlined, Row8.IsHigh, Row8.SelectedCharWidth, 10f, row7Height + row6Height + row5Height + row4Height + row3Height + row2Height + row1Height);
                //ROW9
                var row9Height = Draw(graphics, Row9.Text, SelectedBarCode, Row9.IsBold, Row9.IsUnderlined, Row9.IsHigh, Row9.SelectedCharWidth, 10f, row8Height + row7Height + row6Height + row5Height + row4Height + row3Height + row2Height + row1Height);
                //ROW10
                var row10Height = Draw(graphics, Row10.Text, SelectedBarCode, Row10.IsBold, Row10.IsUnderlined, Row10.IsHigh, Row10.SelectedCharWidth, 10f, row9Height + row8Height + row7Height + row6Height + row5Height + row4Height + row3Height + row2Height + row1Height);
                //ROW11
                var row11Height = Draw(graphics, Row11.Text, SelectedBarCode, Row11.IsBold, Row11.IsUnderlined, Row11.IsHigh, Row11.SelectedCharWidth, 10f, row10Height + row9Height + row8Height + row7Height + row6Height + row5Height + row4Height + row3Height + row2Height + row1Height);
                //ROW12
                var row12Height = Draw(graphics, Row12.Text, SelectedBarCode, Row12.IsBold, Row12.IsUnderlined, Row12.IsHigh, Row12.SelectedCharWidth, 10f, row11Height + row10Height + row9Height + row8Height + row7Height + row6Height + row5Height + row4Height + row3Height + row2Height + row1Height);
                //ROW13
                var row13Height = Draw(graphics, Row13.Text, SelectedBarCode, Row13.IsBold, Row13.IsUnderlined, Row13.IsHigh, Row13.SelectedCharWidth, 10f, row12Height + row11Height + row10Height + row9Height + row8Height + row7Height + row6Height + row5Height + row4Height + row3Height + row2Height + row1Height);
                //ROW14
                var row14Height = Draw(graphics, Row14.Text, SelectedBarCode, Row14.IsBold, Row14.IsUnderlined, Row14.IsHigh, Row14.SelectedCharWidth, 10f, row13Height + row12Height + row11Height + row10Height + row9Height + row8Height + row7Height + row6Height + row5Height + row4Height + row3Height + row2Height + row1Height);
                //ROW15
                var row15Height = Draw(graphics, Row15.Text, SelectedBarCode, Row15.IsBold, Row15.IsUnderlined, Row15.IsHigh, Row15.SelectedCharWidth, 10f, row14Height + row13Height + row12Height + row11Height + row10Height + row9Height + row8Height + row7Height + row6Height + row5Height + row4Height + row3Height + row2Height + row1Height);

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

        public float Draw(Graphics graphics, string input, string barcode, bool isBold, bool isUnderLine, bool isHigh, int selectedCharwidth, float x, float y)
        {
            var labels = GetLabels(input);

            var rowHeight = y;

            foreach (var label in labels)
            {
                if (Regex.IsMatch(label, "^<BAR.*>"))
                {
                    string barCodeLabel = Regex.Replace(label, "<BAR|\\++>|>", "");

                    if (barCodeLabel.Length > 0)
                    {
                        var barcodeImage = _barcodeHelper.GetBarcode(barcode, barCodeLabel, CodeSize, HeightOfCode);
                        graphics.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);

                        x = barcodeImage.Width + 5f;

                        if (barcodeImage.Height > rowHeight)
                        {
                            rowHeight = barcodeImage.Height;
                        }
                    }
                }
                else if (Regex.IsMatch(label, "^<IMG.*>"))
                {
                    var imageLabel = Regex.Replace(label, "<IMG|>", "");

                    if (File.Exists($"{imageLabel}.bmp"))
                    {
                        var image = Image.FromFile($"{imageLabel}.bmp");
                        graphics.DrawImage(image, x, y, image.Width, image.Height);

                        x = image.Width + 5f;

                        if (image.Height > rowHeight)
                        {
                            rowHeight = image.Height;
                        }
                    }
                    else
                    {
                        var font = GetRowFont(isBold, isUnderLine, isHigh, selectedCharwidth);

                        graphics.DrawString("<?>", font, Brushes.Black, new PointF(x, y));

                        x += label.Length * font.Size;
                    }

                }
                else if (!string.IsNullOrEmpty(label))
                {
                    var font = GetRowFont(isBold, isUnderLine, isHigh, selectedCharwidth);

                    graphics.DrawString(label, font, Brushes.Black, new PointF(x, y));

                    x += label.Length * font.Size;

                    if (font.Height > rowHeight)
                    {
                        rowHeight = font.Height;
                    }
                }
            }

            return rowHeight + y;
        }

        List<string> GetLabels(string input)
        {
            var results = new List<string>();

            var labels = Regex.Split(input, "<[^>]*>");

            var matches = Regex.Matches(input, "<[^>]*>");

            for (var i = 0; i < labels.Length; i++)
            {
                results.Add(labels[i]);
                if (i < matches.Count)
                {
                    results.Add(matches[i].Value);
                }
            }

            return results;
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
            HeightOfCode = 5;
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