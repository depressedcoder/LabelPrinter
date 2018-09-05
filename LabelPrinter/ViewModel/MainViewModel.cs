using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows;
using LabelPrinter.Model;
using LabelPrinter.Drawing;
using Newtonsoft.Json;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel
    {
        public void SetLabel()
        {
            if(string.IsNullOrEmpty(Label.SelectedLabelName))
                return;

            var storage = _storageSelector.GetStorage();

            var label = storage.GetLabel(Label.SelectedLabelName);

            if(label !=null)
                Label = label;
        }

        public void PreviewLabel()
        {
            using (var bitmap = new Bitmap(Label.LabelWidth, Label.LabelHeight))
            {
                var rowHeight = 10f;

                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);

                    //Draw all rows
                    foreach (var row in Label.Rows)
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

        void Draw(Graphics graphics, Row row, float x, ref float y)
        {
            var placeholers = GetPlaceholders(row.Text);

            var rowHeight = 10;

            var strategySelector = new DrawingSelector
            {
                Graphics = graphics,
                Barcode = Label.Barcode,
                Row = row
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
            //StreamReader r = new StreamReader("Configure.json");
            
            //string json = r.ReadToEnd();
            //Config con = JsonConvert.DeserializeObject<Config>(json);

            //Setup
            PaperMode value = PaperMode.PlainPaperLabel;
            _printer.Config.LabelMode(value, 40, 3); //40->Label.LabelHeight 
            _printer.Config.LabelWidth(54); //Label.LabelWidth
            _printer.Config.Dark(10); //con.Density
            _printer.Config.Speed(3); //con.Speed
            _printer.Config.PageNo(1);
            _printer.Config.CopyNo(Label.HowManyCoppies);

            _printer.Open(PortType.USB);

            //Print
            _printer.Command.Start();

            var posX = 10;
            var posY = 10;
            var rowHeight = 10;

            foreach (var labelRow in Label.Rows)
            {
                var placeholers = GetPlaceholders(labelRow.Text);
                
                var strategySelector = new DrawingSelector
                {
                    Graphics = Graphics.FromImage(new Bitmap(Label.LabelWidth, Label.LabelHeight)),
                    Barcode = Label.Barcode,
                    Row = labelRow
                };

                foreach (var placeholer in placeholers)
                {
                    var drawingStrategy = strategySelector.GetStrategy(placeholer);

                    drawingStrategy.Print(_printer, ref rowHeight, ref posX, posY);
                }

                posY += rowHeight;
            }

            _printer.Command.End();
            _printer.Close();
        }

        void SetUpCommand()
        {
            SetUpWindow window = new SetUpWindow();
            window.ShowDialog();
        }

        void NewCommand()
        {
            Label.SelectedLabelName = string.Empty;

            foreach (var labelRow in Label.Rows)
            {
                labelRow.Text = string.Empty;
                labelRow.IsBold = false;
                labelRow.IsHigh = false;
                labelRow.IsUnderlined = false;
                labelRow.SelectedCharWidth = CharWidths[0];
            }
        }

        void SaveCommand()
        {
            if (Label.SelectedLabelName != null)
            {
                var storage = _storageSelector.GetStorage();
                storage.SaveLabel(Label);

                LabelSource = storage.GetLabelNames();

                MessageBox.Show("Label saved successfully", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Please Enter Label Name", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        void GetLabelNames()
        {
            var storage = _storageSelector.GetStorage();
            LabelSource = storage.GetLabelNames();
        }
    }
}