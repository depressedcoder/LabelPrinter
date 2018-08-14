using System.Collections.Generic;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using LabelPrinter.Helpers;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        public void PreviewLabel()
        {
            //Add Row 1
            var bitmap = new Bitmap(LabelWidth, LabelHeight);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);

                //Row 1
                var row1Height = Draw(graphics, Row1.Text, SelectedBarCode, Row1.IsBold, Row1.IsUnderlined, Row1.IsHigh, Row1.SelectedCharWidth, 10f, 10f);
                //Row 2
                var row2Height = Draw(graphics, Row2.Text, SelectedBarCode, Row2.IsBold, Row2.IsUnderlined, Row2.IsHigh, Row2.SelectedCharWidth, 10f, row1Height);
                //Row 3
                var row3Height = Draw(graphics, Row3.Text, SelectedBarCode, Row3.IsBold, Row3.IsUnderlined, Row3.IsHigh, Row3.SelectedCharWidth, 10f, row2Height);
                //Row 4
                var row4Height = Draw(graphics, Row4.Text, SelectedBarCode, Row4.IsBold, Row4.IsUnderlined, Row4.IsHigh, Row4.SelectedCharWidth, 10f, row3Height);
                //Row5
                var row5Height = Draw(graphics, Row5.Text, SelectedBarCode, Row5.IsBold, Row5.IsUnderlined, Row5.IsHigh, Row5.SelectedCharWidth, 10f, row4Height);
                //ROW6
                var row6Height = Draw(graphics, Row6.Text, SelectedBarCode, Row6.IsBold, Row6.IsUnderlined, Row6.IsHigh, Row6.SelectedCharWidth, 10f, row5Height);
                //ROW7
                var row7Height = Draw(graphics, Row7.Text, SelectedBarCode, Row7.IsBold, Row7.IsUnderlined, Row7.IsHigh, Row7.SelectedCharWidth, 10f, row6Height);
                //ROW8
                var row8Height = Draw(graphics, Row8.Text, SelectedBarCode, Row8.IsBold, Row8.IsUnderlined, Row8.IsHigh, Row8.SelectedCharWidth, 10f, row7Height);
                //ROW9
                var row9Height = Draw(graphics, Row9.Text, SelectedBarCode, Row9.IsBold, Row9.IsUnderlined, Row9.IsHigh, Row9.SelectedCharWidth, 10f, row8Height);
                //ROW10
                var row10Height = Draw(graphics, Row10.Text, SelectedBarCode, Row10.IsBold, Row10.IsUnderlined, Row10.IsHigh, Row10.SelectedCharWidth, 10f, row9Height);
                //ROW11
                var row11Height = Draw(graphics, Row11.Text, SelectedBarCode, Row11.IsBold, Row11.IsUnderlined, Row11.IsHigh, Row11.SelectedCharWidth, 10f, row10Height);
                //ROW12
                var row12Height = Draw(graphics, Row12.Text, SelectedBarCode, Row12.IsBold, Row12.IsUnderlined, Row12.IsHigh, Row12.SelectedCharWidth, 10f, row11Height);
                //ROW13
                var row13Height = Draw(graphics, Row13.Text, SelectedBarCode, Row13.IsBold, Row13.IsUnderlined, Row13.IsHigh, Row13.SelectedCharWidth, 10f, row12Height);
                //ROW14
                var row14Height = Draw(graphics, Row14.Text, SelectedBarCode, Row14.IsBold, Row14.IsUnderlined, Row14.IsHigh, Row14.SelectedCharWidth, 10f, row13Height);
                //ROW15
                var row15Height = Draw(graphics, Row15.Text, SelectedBarCode, Row15.IsBold, Row15.IsUnderlined, Row15.IsHigh, Row15.SelectedCharWidth, 10f, row14Height);

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
        /// <summary>
        /// Used For Drawing the Barcodes,Images and texts
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="input">All the Input from a particular text field</param>
        /// <param name="barcode">Design of the barcode</param>
        /// <param name="isBold">Text Bold</param>
        /// <param name="isUnderLine">Text UnderLined</param>
        /// <param name="isHigh">If True then returns the double of selected char Width value</param>
        /// <param name="selectedCharwidth">Text Font Size</param>
        /// <param name="x">Drawing position of the Label</param>
        /// <param name="y">Drawing position of the Label</param>
        /// <returns>Returns the Next Rows starting point of Y coordinate</returns>
        float Draw(Graphics graphics, string input, string barcode, bool isBold, bool isUnderLine, bool isHigh, int selectedCharwidth, float x, float y)
        {
            var labels = GetLabels(input);

            var rowHeight = 10;

            foreach (var label in labels)
            {
                if (Regex.IsMatch(label, "^<BAR.*>"))
                {
                    using (var barcodeHelper = new BarcodeHelper())
                    {
                        var barcodeImage = barcodeHelper.GetBarcode(barcode,label, CodeSize, HeightOfCode);
                        graphics.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);

                        x += barcodeImage.Width;

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

                        x += image.Width;

                        if (image.Height > rowHeight)
                        {
                            rowHeight = image.Height;
                        }
                    }
                    else if(label == "<IMG>")
                    {
                        var image = Image.FromFile("Norsel.bmp");
                        graphics.DrawImage(image, x, y, image.Width, image.Height);

                        x += image.Width;

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
                else
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

            if (string.IsNullOrEmpty(input))
            {
                return results;
            }

            var matches = Regex.Matches(input, "<.+?>");

            if (matches.Count == 0)
            {
                results.Add(input);
                return results;
            }

            for (var i = 0; i < matches.Count; i++)
            {
                var idx = input.IndexOf(matches[i].Value, System.StringComparison.Ordinal);

                var token = input.Substring(0, idx);

                if (!string.IsNullOrEmpty(token))
                {
                    results.Add(token);
                }

                results.Add(matches[i].Value);

                input = input.Substring(idx + matches[i].Value.Length);

                if (!Regex.IsMatch(input, "<.+?>"))
                {
                    if (!string.IsNullOrEmpty(input))
                        results.Add(input);
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
        void UpdateLabel()
        {

        }
        void ExitCommand()
        {
            //Exit Button
        }

        void PrintJobsCommand()
        {
            //PrintJobs Button
        }

        void PrintCommand()
        {
            //Print Button
        }

        void SetUpCommand()
        {
            //Set Up Button
        }

        void NewCommand()
        {
            //New Button
        }

        void SaveCommand()
        {

        }
    }
}