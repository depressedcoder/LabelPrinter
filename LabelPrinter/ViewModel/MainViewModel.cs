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
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json.Linq;

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel
    {
        
        //Showing all the values of rows by selecting the file from label name ComboBox
        public void ValueUpdate()
        {
            string jsonFile = SelectedLabelName + ".json";
            SelectedLabelName += "-IMPORT.txt";
            if (File.Exists(SelectedLabelName))
            {
                var desiredText = File.ReadAllLines(SelectedLabelName);
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

            //For getting the value of selected Char width,ishigh,isbold,isunderline from the json file
            if (File.Exists(jsonFile))
            {
                using (StreamReader r = new StreamReader(jsonFile))
                {
                    string json = r.ReadToEnd();
                    //LabelRow setUp = JsonConvert.DeserializeObject<LabelRow>(json);
                    JArray array = JArray.Parse(json);
                    foreach (JObject obj in array.Children<JObject>())
                    {
                        foreach (JProperty singleProp in obj.Properties())
                        {
                            string name = singleProp.Name;
                            string value = singleProp.Value.ToString();
                            //Assign value the combobox and checkboxes.
                        }
                    }
                }
            }

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
            SaveFileDialog save = new SaveFileDialog();

            save.Filter = "Text files (*.txt)|*-IMPORT.txt|All files (*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK)
            {
                string fileName = Regex.Replace(save.FileName, ".txt", "-IMPORT.txt");
                using (StreamWriter objWriter = new StreamWriter(File.Create(fileName)))
                {
                    objWriter.WriteLine(SelectedLabelName);
                    objWriter.WriteLine(HowManyCoppies);
                    objWriter.WriteLine(Row1.Text);
                    objWriter.WriteLine(Row2.Text);
                    objWriter.WriteLine(Row3.Text);
                    objWriter.WriteLine(Row4.Text);
                    objWriter.WriteLine(Row5.Text);
                    objWriter.WriteLine(Row6.Text);
                    objWriter.WriteLine(Row7.Text);
                    objWriter.WriteLine(Row8.Text);
                    objWriter.WriteLine(Row9.Text);
                    objWriter.WriteLine(Row10.Text);
                    objWriter.WriteLine(Row11.Text);
                    objWriter.WriteLine(Row12.Text);
                    objWriter.WriteLine(Row13.Text);
                    objWriter.WriteLine(Row14.Text);
                    objWriter.WriteLine(Row15.Text);
                    System.Windows.MessageBox.Show("Details have been saved");
                }

                //Saving all Rows isHigh,isBold,IsUnderline,Selected CharWidth in json file
                string strJsonResult = JsonConvert.SerializeObject(
                 new[]
                 {
                         new{Row1.SelectedCharWidth, Row1.IsHigh, Row1.IsBold, Row1.IsUnderlined},
                         new{Row2.SelectedCharWidth, Row2.IsHigh, Row2.IsBold, Row2.IsUnderlined},
                         new{Row3.SelectedCharWidth, Row3.IsHigh, Row3.IsBold, Row3.IsUnderlined},
                         new{Row4.SelectedCharWidth, Row4.IsHigh, Row4.IsBold, Row4.IsUnderlined},
                         new{Row5.SelectedCharWidth, Row5.IsHigh, Row5.IsBold, Row5.IsUnderlined},
                         new{Row6.SelectedCharWidth, Row6.IsHigh, Row6.IsBold, Row6.IsUnderlined},
                         new{Row7.SelectedCharWidth, Row7.IsHigh, Row7.IsBold, Row7.IsUnderlined},
                         new{Row8.SelectedCharWidth, Row8.IsHigh, Row8.IsBold, Row8.IsUnderlined},
                         new{Row9.SelectedCharWidth, Row9.IsHigh, Row9.IsBold, Row9.IsUnderlined},
                         new{Row10.SelectedCharWidth, Row10.IsHigh, Row10.IsBold, Row10.IsUnderlined},
                         new{Row11.SelectedCharWidth, Row11.IsHigh, Row11.IsBold, Row11.IsUnderlined},
                         new{Row12.SelectedCharWidth, Row12.IsHigh, Row12.IsBold, Row12.IsUnderlined},
                         new{Row13.SelectedCharWidth, Row13.IsHigh, Row13.IsBold, Row13.IsUnderlined},
                         new{Row14.SelectedCharWidth, Row14.IsHigh, Row14.IsBold, Row14.IsUnderlined},
                         new{Row15.SelectedCharWidth, Row14.IsHigh, Row15.IsBold, Row15.IsUnderlined}
                   
                 }
           );
                string trimmed = Regex.Replace(fileName, "-IMPORT.txt", "");
                trimmed += ".json";
                File.WriteAllText(trimmed, strJsonResult);
            }
        }
        void getLabelNames()
        {
            using (StreamReader r = new StreamReader("Configure.json"))
            {
                string json = r.ReadToEnd();
                SetUpViewModel setUp = JsonConvert.DeserializeObject<SetUpViewModel>(json);
                if (setUp.SelectedDataConnection == "Text Files")
                {
                    //if the no path is selected
                    if(setUp.LocationOfFile == null)
                    {
                        DirectoryInfo d = new DirectoryInfo(@"C:\Users\BS229\Source\Repos\LabelPrinter2\LabelPrinter\bin\Debug");

                        //getting all the txt file that named with IMPORT
                        FileInfo[] Files = d.GetFiles("*-IMPORT.txt");
                        LabelName = new List<string> { "" };

                        //adding all the file names in Label Name Combobox
                        foreach (FileInfo file in Files)
                        {
                            var item = file.Name;
                            string trimmed = Regex.Replace(item, "-IMPORT.txt", "");
                            LabelName.Add(trimmed);
                        }
                    }
                    else
                    {
                        DirectoryInfo d = new DirectoryInfo(setUp.LocationOfFile);

                        //getting all the txt file that named with IMPORT
                        FileInfo[] Files = d.GetFiles("*-IMPORT.txt");
                        LabelName = new List<string> { "" };

                        //adding all the file names in Label Name Combobox
                        foreach (FileInfo file in Files)
                        {
                            var item = file.Name;
                            string trimmed = Regex.Replace(item, "-IMPORT.txt", "");
                            LabelName.Add(trimmed);
                        }

                    }
                }
            }
        }
    }
}