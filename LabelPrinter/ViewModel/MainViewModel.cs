using LabelPrinter.Drawing;
using LabelPrinter.Model;
using LabelPrinter.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media.Imaging;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel
    {
        public void SetLabel()
        {
            if (string.IsNullOrEmpty(Label.SelectedLabelName))
            {
                return;
            }

            var storage = _storageSelector.GetStorage();

            var label = storage.GetLabel(Label.SelectedLabelName);

            if (label != null)
            {
                Label = label;
            }
        }

        public void PreviewLabel()
        {
            using (var bitmap = new Bitmap((AppConstant.ImageMultiplyWith * Label.LabelWidth), (AppConstant.ImageMultiplyWith * Label.LabelHeight)))
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

        private void Draw(Graphics graphics, Row row, float x, ref float y)
        {
            var placeholers = GetPlaceholders(row.Text);

            var rowHeight = 10;

            var storage = _storageSelector.GetStorage();
            var weight = storage.GetLabels(Label.SelectedLabelName)?.Wieght;
            var strategySelector = new DrawingSelector
            {
                Graphics = graphics,
                Barcode = Label.Barcode,
                Weight = weight ?? 0,
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

        private List<string> GetPlaceholders(string input)
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
                    {
                        results.Add(input);
                    }
                }
            }

            return results;
        }

        private void UpdateLabel()
        {

        }

        private void ExitCommand()
        {
            System.Windows.Application.Current.MainWindow.Close();
        }

        private void PrintJobsCommand()
        {
            PrintJobWindow window = new PrintJobWindow();
            window.ShowDialog();
        }

        private void PrintCommand()
        {
            PhysicalPrinter.Print(Label);
        }

        private void Print()
        {
            var con = StorageSelector.GetConfig();

            SetupPrinter(con);

            //Printer start
            _printer.Command.Start();

            var posY = 10;
            var rowHeight = 10;
            var storage = _storageSelector.GetStorage();

            foreach (var labelRow in Label.Rows)
            {
                var posX = 10;
                var placeholers = GetPlaceholders(labelRow.Text);

                var strategySelector = new DrawingSelector
                {
                    Graphics = Graphics.FromImage(new Bitmap(Label.LabelWidth, Label.LabelHeight)),
                    Barcode = Label.Barcode,
                    Weight = storage.GetLabels(Label.SelectedLabelName)?.Wieght ?? 0,
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

        private void SetupPrinter(Config con)
        {
            // Connect printer
            PortType port = EnumsConverter.ParseEnum<PortType>(string.IsNullOrEmpty(con.SelectedPrinterPort) ? "USB" : con.SelectedPrinterPort);
            _printer.Open(port);
            //Setup
             
            PaperMode value = EnumsConverter.ParseEnum<PaperMode>(con.RadioButtonValue.ToString());
            int gapFeed = Convert.ToInt32(string.IsNullOrEmpty(con.GapControlText) ? "0" : con.GapControlText);
            _printer.Config.LabelMode(value, Label.LabelHeight, gapFeed); //40-> 
            _printer.Config.LabelWidth(Label.LabelWidth); //Label.LabelWidth
            _printer.Config.Dark(con.Density); //con.Density
            _printer.Config.Speed(con.Speed); //con.Speed
            _printer.Config.PageNo(1);
            _printer.Config.CopyNo(Label.HowManyCoppies);
            ////Cutting command
            if (Label.IsAutomaticCuttingDevice)
            {
                _printer.Config.CutPapaerOn(1);
            }
            else
            {
                _printer.Config.CutPapaerOff();
            }

            _printer.Config.SetLeftMargin(Label.DistanceFromLeft); //8 dot = 1 mm
        }

        private void SetUpCommand()
        {
            SetUpWindow window = new SetUpWindow();
            window.Closing += Window_Closing;
            window.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var storage = _storageSelector.GetStorage();
            LabelSource = storage.GetLabelNames();
        }

        private void NewCommand()
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

        private void SaveCommand()
        {
            if (Label.SelectedLabelName != null)
            {
                var storage = _storageSelector.GetStorage(); 
                storage.SaveLabel(Label);

                LabelSource = storage.GetLabelNames();

                MessageView.Instance.ShowInformation("Label saved successfully");
            }
            else
            {
                MessageView.Instance.ShowWarning("Please Enter Label Name");
            }
        }


        private void DeleteCommand()
        {
            var isSure = MessageBox.Show("Are you sure to delete this label?", "Sure?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(isSure == MessageBoxResult.Yes)
            {
                var storage = _storageSelector.GetStorage();
                storage.DeleteLabel(Label);
                LabelSource = storage.GetLabelNames();
                if(LabelSource != null && LabelSource.Count > 0)
                {
                    Label = storage.GetLabel(LabelSource.FirstOrDefault() ?? string.Empty);
                }
            }            
        }

        private void GetLabelNames()
        {
            var storage = _storageSelector.GetStorage();
            LabelSource = storage.GetLabelNames();
        }
    }
}