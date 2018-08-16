using System.Collections.Generic;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System;
using System.Windows.Forms;
using LabelPrinter.LabelDrawingStrategy;
using LabelPrinter.Model;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel
    {
        public void PreviewLabel()
        {
            //Add Row 1
            var bitmap = new Bitmap(LabelWidth, LabelHeight);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);

                //Row 1
                var row1Height = Draw(graphics,  Barcode, Row1, 10f, 10f);
                //Row 2
                var row2Height = Draw(graphics,  Barcode, Row2, 10f, row1Height);
                //Row 3
                var row3Height = Draw(graphics, Barcode, Row3, 10f, row2Height);
                //Row 4
                var row4Height = Draw(graphics, Barcode, Row4 , 10f, row3Height);
                //Row5
                var row5Height = Draw(graphics, Barcode, Row5 , 10f, row4Height);
                //ROW6
                var row6Height = Draw(graphics, Barcode, Row6 , 10f, row5Height);
                //ROW7
                var row7Height = Draw(graphics, Barcode, Row7, 10f, row6Height);
                //ROW8
                var row8Height = Draw(graphics, Barcode, Row8 , 10f, row7Height);
                //ROW9
                var row9Height = Draw(graphics, Barcode, Row9 , 10f, row8Height);
                //ROW10
                var row10Height = Draw(graphics, Barcode, Row10, 10f, row9Height);
                //ROW11
                var row11Height = Draw(graphics, Barcode, Row11, 10f, row10Height);
                //ROW12
                var row12Height = Draw(graphics, Barcode, Row12, 10f, row11Height);
                //ROW13
                var row13Height = Draw(graphics, Barcode, Row13, 10f, row12Height);
                //ROW14
                var row14Height = Draw(graphics, Barcode, Row14, 10f, row13Height);
                //ROW15
                var row15Height = Draw(graphics, Barcode, Row15 , 10f, row14Height);

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
        /// <param name="barcode">Design of the barcode</param>
        /// <param name="x">Drawing position of the Label</param>
        /// <param name="y">Drawing position of the Label</param>
        /// <returns>Returns the Next Rows starting point of Y coordinate</returns>
        float Draw(Graphics graphics, Barcode barcode, LabelRow row, float x, float y)
        {
            var labels = GetLabels(row.Text);

            var rowHeight = 10;

            foreach (var label in labels)
            {

                if (Regex.IsMatch(label, "^<BAR.*>"))
                {
                    var s = new StrategySelector().GetStrategy(label).Draw(graphics, barcode, row, x, y);

                    //using (var barcodeHelper = new BarcodeHelper())
                    //{
                    //    var barcodeImage = barcodeHelper.GetBarcode(barcode,label, CodeSize, HeightOfCode);
                    //    graphics.DrawImage(barcodeImage, x, y, barcodeImage.Width, barcodeImage.Height);

                    //    x += barcodeImage.Width;

                    //    if (barcodeImage.Height > rowHeight)
                    //    {
                    //        rowHeight = barcodeImage.Height;
                    //    }
                    //}
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
                    else if (label == "<IMG>")
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
                        var font = GetRowFont(row.IsBold, row.IsUnderlined, row.IsHigh, row.SelectedCharWidth);

                        graphics.DrawString("<?>", font, Brushes.Black, new PointF(x, y));

                        x += label.Length * font.Size;
                    }
                }
                else if (label == "<WEIGHT>")
                {
                    var font = GetRowFont(row.IsBold, row.IsUnderlined , row.IsHigh, row.SelectedCharWidth);

                    graphics.DrawString("0.0", font, Brushes.Black, new PointF(x, y));

                    x += label.Length * font.Size;

                    if (font.Height > rowHeight)
                    {
                        rowHeight = font.Height;
                    }
                }
                else if (label == "<TIMESTAMP>")
                {
                    string timestamp = DateTime.Now.ToString("h:mm");

                    var font = GetRowFont(row.IsBold, row.IsUnderlined, row.IsHigh, row.SelectedCharWidth);

                    graphics.DrawString(timestamp, font, Brushes.Black, new PointF(x, y));

                    x += label.Length * font.Size;

                    if (font.Height > rowHeight)
                    {
                        rowHeight = font.Height;
                    }
                }
                else if (label == "<TIME>")
                {
                    string time = DateTime.Now.ToString("HH:mm:ss tt");

                    var font = GetRowFont(row.IsBold, row.IsUnderlined, row.IsHigh, row.SelectedCharWidth);

                    graphics.DrawString(time, font, Brushes.Black, new PointF(x, y));

                    x += label.Length * font.Size;

                    if (font.Height > rowHeight)
                    {
                        rowHeight = font.Height;
                    }
                }
                else if (label == "<DATE>")
                {
                    string date = DateTime.Now.ToString("dd-MM-yyyy");

                    var font = GetRowFont(row.IsBold, row.IsUnderlined, row.IsHigh, row.SelectedCharWidth);

                    graphics.DrawString(date, font, Brushes.Black, new PointF(x, y));

                    x += label.Length * font.Size;

                    if (font.Height > rowHeight)
                    {
                        rowHeight = font.Height;
                    }
                }
                else
                {
                    var font = GetRowFont(row.IsBold, row.IsUnderlined, row.IsHigh, row.SelectedCharWidth);

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
            var desiredText = File.ReadAllLines("NORSEL-IMPORT.txt");
            if (desiredText.Length > 0)
                Row1.Text = desiredText[0];
            if (desiredText.Length > 1)
                Row2.Text = desiredText[1];
            if (desiredText.Length > 2)
                Row3.Text = desiredText[2];
        }

        void SaveCommand()
        {
            using (StreamWriter objWriter = new StreamWriter("NORSEL-IMPORT.txt"))
            {
                objWriter.Write(Row1.Text + "\n");
                objWriter.Write(Row2.Text + "\n");
                objWriter.Write(Row3.Text);
                MessageBox.Show("Details have been saved");
            }
        }
    }
}