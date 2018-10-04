using LabelPrinter.Drawing;
using LabelPrinter.Model;
using LabelPrinter.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Permissions;
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
            Config config = StorageSelector.GetConfig();
            WatchDirectory(config.LocationOfFile);
            //if(config.SelectedConnection == StorageTypes.Text)
            //{

            //}
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void WatchDirectory(string textConnection)
        {
            if (string.IsNullOrEmpty(textConnection))
            {
                return;
            }

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = textConnection;
            /* Watch for changes in LastAccess and LastWrite times, and
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
               | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            // Only watch text files.
            watcher.Filter = "*.txt";

            // Add event handlers.
            watcher.Changed += new FileSystemEventHandler(OnCreated);
            watcher.Created += new FileSystemEventHandler(OnCreated);
            //watcher.Deleted += new FileSystemEventHandler(OnChanged);
            //watcher.Renamed += new RenamedEventHandler(OnRenamed);

            // Begin watching.
            watcher.EnableRaisingEvents = true;
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string textFilePath = e.FullPath;
            string text = string.Empty;
            if (!File.Exists(textFilePath))
            {
                return;
            }
            if (e.Name.Contains("LABEL-IMPORT"))
            {
                string jsonData = string.Empty;
                string basePath = AppDomain.CurrentDomain.BaseDirectory +"Norsel.json";
                string fileNameOfLabel = basePath;
                if (!File.Exists(fileNameOfLabel))
                {
                    return;
                }
                else
                {
                    jsonData = File.ReadAllText(fileNameOfLabel);
                }

                text = File.ReadAllText(textFilePath);

                if (!string.IsNullOrEmpty(text))
                {
                    var labelRowLines = text.Split('\n');
                    for (int i = 3; i <= labelRowLines.Length; i++)
                    {
                        var param = labelRowLines[i - 1].Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                        string origin = $"<I{i - 2}>";
                        jsonData = jsonData.Replace(origin, param);
                    }

                    File.Delete(textFilePath);

                    Label label = JsonConvert.DeserializeObject<Label>(jsonData);
                    if (label != null)
                    {
                        Label = label;
                        PhysicalPrinter.Instance.Print(label);
                    }
                }
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

        private void WriteOutput(object sender, RoutedEventArgs e)
        {
            string fileName = "PrintingHistory_" + DateTime.Today + "xlsx";
            // Delete contents of the temporary directory.
            XlsxRW.DeleteDirectoryContents(tempDir);

            // Unzip template XLSX file to the temporary directory.
            XlsxRW.UnzipFile(templateFile, tempDir);

            // We will need two string tables; a lookup IDictionary<string, int> for fast searching and 
            // an ordinary IList<string> where items are sorted by their index.
            IDictionary<string, int> lookupTable;

            // Call helper methods which creates both tables from input data.
            var stringTable = XlsxRW.CreateStringTables(this.data, out lookupTable);

            // Create XML file..
            using (var stream = new FileStream(Path.Combine(tempDir, @"xl\sharedStrings.xml"), FileMode.Create))
                // ..and fill it with unique strings used in the workbook
                XlsxRW.WriteStringTable(stream, stringTable);

            // Create XML file..
            using (var stream = new FileStream(Path.Combine(tempDir, @"xl\worksheets\sheet1.xml"),
                FileMode.Create))
                // ..and fill it with rows and columns of the DataTable.
                XlsxRW.WriteWorksheet(stream, this.data, lookupTable);

            // ZIP temporary directory to the XLSX file.
            XlsxRW.ZipDirectory(tempDir, fileName);

            // If checkbox is checked, show XLSX file in Microsoft Excel.
            if (true)
                System.Diagnostics.Process.Start(fileName);
        }


        #endregion

    }
}