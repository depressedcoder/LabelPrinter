using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using LabelPrinter.Model;
using System.Linq;
using LabelPrinter.Drawing;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel
    {
        public void SetLabel()
        {
            var storage = _storageSelector.GetStorage();

            var labelDetails = storage.GetLabelDetails(SelectedLabelName);

            var labelRows = GetType().GetProperties()
                .Where(p => p.GetValue(this, new object[] { })?.GetType() == typeof(LabelRow))
                .Select(p => (LabelRow)p.GetValue(this, new object[] { })).ToList();

            foreach (var labelRow in labelRows)
            {
                var idx = labelRows.IndexOf(labelRow);

                var labelRowDetail = labelDetails.LabelRows.ElementAtOrDefault(idx);

                if (labelRowDetail != null)
                {
                    labelRow.Text = labelRowDetail.Text;
                    labelRow.CharWidth = labelRowDetail.CharWidth;
                    labelRow.IsHigh = labelRowDetail.IsHigh;
                    labelRow.IsBold = labelRowDetail.IsBold;
                    labelRow.IsUnderlined = labelRowDetail.IsUnderlined;
                }
            }

            HowManyCoppies = labelDetails.NumberOfCopies;
        }

        public void PreviewLabel()
        {
            using (var bitmap = new Bitmap(LabelWidth, LabelHeight))
            {
                var rowHeight = 10f;

                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);

                    //Find all rows
                    var rows = GetType().GetProperties()
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

            var strategySelector = new DrawingSelector
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
            System.Windows.Application.Current.MainWindow.Close();
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
            SetUpWindow window = new SetUpWindow();
            window.ShowDialog();
        }

        void NewCommand()
        {
            SelectedLabelName = string.Empty;

            var rows = GetType().GetProperties()
                .Where(p => p.GetValue(this, new object[] { })?.GetType() == typeof(LabelRow))
                .Select(p => (LabelRow)p.GetValue(this, new object[] { }));

            foreach (var labelRow in rows)
            {
                labelRow.Text = string.Empty;
            }
        }

        void SaveCommand()
        {
            var storage = _storageSelector.GetStorage();

            var labelRows = GetType().GetProperties()
                .Where(p => p.GetValue(this, new object[] { })?.GetType() == typeof(LabelRow))
                .Select(p => (LabelRow)p.GetValue(this, new object[] { }));

            storage.SaveLabel(SelectedLabelName, HowManyCoppies, labelRows);

            LabelSource = storage.GetLabelNames();
        }

        void GetLabelNames()
        {
            var storage = _storageSelector.GetStorage();
            LabelSource = storage.GetLabelNames();
        }
    }
}