using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LabelPrinter.Model;
using System.Linq;
using LabelPrinter.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using LabelPrinter.Storage;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel
    {

        //Showing all the values of rows by selecting the file from label name ComboBox
        public void ValueUpdate()
        {
            var StrategySelector = new StorageSelector();
            using (StreamReader r = new StreamReader("Configure.json"))
            {
                string json = r.ReadToEnd();
                Config con = JsonConvert.DeserializeObject<Config>(json);
                var storageStrategy = StrategySelector.GetStorage(con.SelectedDataConnection);
                string[] desiredText = storageStrategy.GetLabelDetails(SelectedLabelName);

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

            ////For getting the value of selected Char width,ishigh,isbold,isunderline from the json file
            //if (File.Exists(jsonFile))
            //{
            //    var rowInfo = JsonConvert.DeserializeObject<IList<LabelRow>>(File.ReadAllText(jsonFile));
            //    //using (StreamReader r = new StreamReader(jsonFile))
            //    //{
            //    //    string json = r.ReadToEnd();
            //    //    // MainViewModel main = JsonConvert.DeserializeObject<MainViewModel>(json);
            //    //    JArray array = JArray.Parse(json);
            //    //    foreach (JObject obj in array.Children<JObject>())
            //    //    {
            //    //        foreach (JProperty singleProp in obj.Properties())
            //    //        {
            //    //            string name = singleProp.Name;
            //    //            string value = singleProp.Value.ToString();
            //    //            //Assign value the combobox and checkboxes.
            //    //        }
            //    //    }
            //    //}


            //}

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
            SelectedLabelName = "";
            Row1.Text = "";
            Row2.Text = "";
            Row3.Text = "";
            Row4.Text = "";
            Row5.Text = "";
            Row6.Text = "";
            Row7.Text = "";
            Row8.Text = "";
            Row9.Text = "";
            Row10.Text = "";
            Row11.Text = "";
            Row12.Text = "";
            Row13.Text = "";
            Row14.Text = "";
            Row15.Text = "";
        }

        void SaveCommand()
        {
            var StrategySelector = new StorageSelector();
            using (StreamReader r = new StreamReader("Configure.json"))
            {
                string json = r.ReadToEnd();
                Config setUp = JsonConvert.DeserializeObject<Config>(json);
                var storageStrategy = StrategySelector.GetStorage(setUp.SelectedDataConnection);

                var allLines = GetType().GetProperties()
                    .Where(p => p.GetValue(this, new object[] { })?.GetType() == typeof(LabelRow))
                    .Select(p => (LabelRow)p.GetValue(this, new object[] { }));
                
                storageStrategy.SaveLabel(ComBoxLabelName, HowManyCoppies,allLines);
            }
        }

        void GetLabelNames()
        {
            var StrategySelector = new StorageSelector();
            using (StreamReader r = new StreamReader("Configure.json"))
            {
                string json = r.ReadToEnd();
                Config con = JsonConvert.DeserializeObject<Config>(json);
                var storageStrategy = StrategySelector.GetStorage(con.SelectedDataConnection);
                LabelName = storageStrategy.GetLabelNames();
            }

            //using (StreamReader r = new StreamReader("Configure.json"))
            //{
            //    string json = r.ReadToEnd();
            //    SetUpViewModel setUp = JsonConvert.DeserializeObject<SetUpViewModel>(json);
            //    if (setUp.SelectedDataConnection == "Text Files")
            //    {
            //        //if the no path is selected
            //        if (setUp.LocationOfFile == null)
            //        {
            //            DirectoryInfo d = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory);

            //            //getting all the txt file that named with IMPORT
            //            FileInfo[] Files = d.GetFiles("*-IMPORT.txt");
            //            LabelName = new List<string> { "" };

            //            //adding all the file names in Label Name Combobox
            //            foreach (FileInfo file in Files)
            //            {
            //                var item = file.Name;
            //                string trimmed = Regex.Replace(item, "-IMPORT.txt", "");
            //                LabelName.Add(trimmed);
            //            }
            //        }
            //        else
            //        {
            //            DirectoryInfo d = new DirectoryInfo(setUp.LocationOfFile);

            //            //getting all the txt file that named with IMPORT
            //            FileInfo[] Files = d.GetFiles("*-IMPORT.txt");
            //            LabelName = new List<string> { "" };

            //            //adding all the file names in Label Name Combobox
            //            foreach (FileInfo file in Files)
            //            {
            //                var item = file.Name;
            //                string trimmed = Regex.Replace(item, "-IMPORT.txt", "");
            //                LabelName.Add(trimmed);
            //            }
            //        }
            //    }
            //}
        }
    }
}