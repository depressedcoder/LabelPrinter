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
        #region public method(s)

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

        #endregion

        #region private method(s)

        private void Draw(Graphics graphics, Row row, float x, ref float y)
        {
            var placeholers = PhysicalPrinter.Instance.GetPlaceholders(row.Text);

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
            PhysicalPrinter.Instance.Print(Label);
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
            if (isSure == MessageBoxResult.Yes)
            {
                var storage = _storageSelector.GetStorage();
                storage.DeleteLabel(Label);
                LabelSource = storage.GetLabelNames();
                if (LabelSource != null && LabelSource.Count > 0)
                {
                    Label = storage.GetLabel(LabelSource.FirstOrDefault() ?? string.Empty);
                }
            }
        }
         
        #endregion

    }
}