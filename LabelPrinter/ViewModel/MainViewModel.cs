using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
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
                var row1Height = Draw(graphics, Barcode, Row1, 10f, 10f);
                //Row 2
                var row2Height = Draw(graphics, Barcode, Row2, 10f, row1Height);
                //Row 3
                var row3Height = Draw(graphics, Barcode, Row3, 10f, row2Height);
                //Row 4
                var row4Height = Draw(graphics, Barcode, Row4, 10f, row3Height);
                //Row5
                var row5Height = Draw(graphics, Barcode, Row5, 10f, row4Height);
                //ROW6
                var row6Height = Draw(graphics, Barcode, Row6, 10f, row5Height);
                //ROW7
                var row7Height = Draw(graphics, Barcode, Row7, 10f, row6Height);
                //ROW8
                var row8Height = Draw(graphics, Barcode, Row8, 10f, row7Height);
                //ROW9
                var row9Height = Draw(graphics, Barcode, Row9, 10f, row8Height);
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
                var row15Height = Draw(graphics, Barcode, Row15, 10f, row14Height);
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
            var placeholers = GetPlaceholders(row.Text);

            var rowHeight = 10;

            var strategySelector = new StrategySelector();

            foreach (var placeholer in placeholers)
            {
                var drawingStrategy = strategySelector.GetStrategy(placeholer);

                drawingStrategy.Draw(graphics, barcode, row, ref rowHeight, ref x, y);
            }

            return rowHeight + y;
        }

        List<string> GetPlaceholders(string input)
        {
            var placeholderPattern = "<[A-Z]+?>";

            var results = new List<string>();

            if (string.IsNullOrEmpty(input))
            {
                return results;
            }

            var matches = Regex.Matches(input, placeholderPattern);

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

                if (!Regex.IsMatch(input, placeholderPattern))
                {
                    if (!string.IsNullOrEmpty(input))
                        results.Add(input);
                }
            }

            return results;
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
            //var numberOfCopies = HowManyCoppies.ToString();

            if(File.Exists("NORSEL-IMPORT.txt"))
            {
                var desiredText = File.ReadAllLines("NORSEL-IMPORT.txt");
                if (desiredText.Length > 0)
                    SelectedLabelName = desiredText[0];
                if (desiredText.Length > 1)
                    HowManyCoppies = Int32.Parse(desiredText[1]);
                if (desiredText.Length > 2)
                    Row1.Text = desiredText[2];
                if (desiredText.Length > 3)
                    Row2.Text = desiredText[3];
                if (desiredText.Length > 4)
                    Row3.Text = desiredText[4];
                if (desiredText.Length > 5)
                    Row4.Text = desiredText[5];
                if (desiredText.Length > 6)
                    Row5.Text = desiredText[6];
                if (desiredText.Length > 7)
                    Row6.Text = desiredText[7];
                if (desiredText.Length > 8)
                    Row7.Text = desiredText[8];
                if (desiredText.Length > 9)
                    Row8.Text = desiredText[9];
                if (desiredText.Length > 10)
                    Row9.Text = desiredText[10];
                if (desiredText.Length > 11)
                    Row10.Text = desiredText[11];
                if (desiredText.Length > 12)
                    Row11.Text = desiredText[12];
                if (desiredText.Length > 13)
                    Row12.Text = desiredText[13];
                if (desiredText.Length > 14)
                    Row13.Text = desiredText[14];
                if (desiredText.Length > 15)
                    Row14.Text = desiredText[15];
                if (desiredText.Length > 16)
                    Row15.Text = desiredText[16];
            }
            else
            {
                MessageBox.Show("No file exist");
            }

        }

        void SaveCommand()
        {
            using (StreamWriter objWriter = new StreamWriter("NORSEL-IMPORT.txt"))
            {
                objWriter.Write(SelectedLabelName+"\n");
                objWriter.Write(HowManyCoppies + "\n");
                objWriter.Write(Row1.Text + "\n");
                objWriter.Write(Row2.Text + "\n");
                objWriter.Write(Row3.Text + "\n");
                objWriter.Write(Row4.Text + "\n");
                objWriter.Write(Row5.Text + "\n");
                objWriter.Write(Row6.Text + "\n");
                objWriter.Write(Row7.Text + "\n");
                objWriter.Write(Row8.Text + "\n");
                objWriter.Write(Row9.Text + "\n");
                objWriter.Write(Row10.Text + "\n");
                objWriter.Write(Row11.Text + "\n");
                objWriter.Write(Row12.Text + "\n");
                objWriter.Write(Row13.Text + "\n");
                objWriter.Write(Row14.Text + "\n");
                objWriter.Write(Row15.Text + "\n");
                MessageBox.Show("Details have been saved");
            }
        }
    }
}