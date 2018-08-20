using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LabelPrinter.LabelDrawingStrategy;
using LabelPrinter.Model;
using System.Linq;
using LabelPrinter.DataConnectionStrategy;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel
    {
        public void PreviewLabel()
        {
            //Testing StrategySelectorForDataConnection with the Label Name ComboBox.
            string lName = SelectedLabelName;
            var strategy = new StrategySelectorForDataConnection();
            strategy.getConnectionStrategy(lName);

            using (var bitmap = new Bitmap(LabelWidth, LabelHeight))
            {
                var rowHeight = 10f;

                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);

                    //Find all rows
                    var rows = this.GetType().GetProperties()
                        .Where(p => p.GetValue(this, new object[] { })?.GetType() == typeof(LabelRow))
                        .Select(p => (LabelRow)p.GetValue(this, new object[] { }));

                    //Draw all rows
                    foreach (var row in rows)
                    {
                        Draw(graphics, row, 10f, ref rowHeight);
                    }
                }

                //Draw preview image
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
        }

        void Draw(Graphics graphics, LabelRow row, float x, ref float y)
        {
            var placeholers = GetPlaceholders(row.Text);

            var rowHeight = 10;

            var strategySelector = new StrategySelector
            {
                Graphics = graphics,
                Barcode = Barcode,
                LabelRow = row
            };

            //Draw all placeholders
            foreach (var placeholer in placeholers)
            {
                var drawingStrategy = strategySelector.GetStrategy(placeholer);

                drawingStrategy.Draw(ref rowHeight, ref x, y);
            }

            //Increase row width
            y = rowHeight + y;
        }

        List<string> GetPlaceholders(string input)
        {
            var placeholderPattern = "<([a-zA-Z0-9_.-])+?>";

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
            var window = new SetUpWindow();
            window.ShowDialog();
        }

        void NewCommand()
        {
            if (File.Exists("NORSEL-IMPORT.txt"))
            {
                var desiredText = File.ReadAllLines("NORSEL-IMPORT.txt");
                if (desiredText.Length > 0)
                    SelectedLabelName = desiredText[0];
                if (desiredText.Length > 1)
                    HowManyCoppies = int.Parse(desiredText[1]);
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
                objWriter.Write(SelectedLabelName + "\n");
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