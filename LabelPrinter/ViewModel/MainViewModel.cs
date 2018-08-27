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

namespace LabelPrinter.ViewModel
{
    public partial class MainViewModel
    {
        public void ValueUpdate()
        {
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
            else
            {
                System.Windows.MessageBox.Show("No file exist");
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
            
        }

        void SaveCommand()
        {
            SaveFileDialog save = new SaveFileDialog();

            save.Filter = "Text files (*.txt)|*-IMPORT.txt|All files (*.*)|*.*";
            
            if (save.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter objWriter = new StreamWriter(File.Create(save.FileName)))
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
            }
        }
        void getLabelNames()
        {
            using (StreamReader r = new StreamReader("Configure.json"))
            {
                string json = r.ReadToEnd();
                SetUpViewModel account = JsonConvert.DeserializeObject<SetUpViewModel>(json);
                if (account.SelectedDataConnection == "Text Files")
                {
                    DirectoryInfo d = new DirectoryInfo(@"C:\Users\BS229\Source\Repos\LabelPrinter2\LabelPrinter\bin\Debug");
                    FileInfo[] Files = d.GetFiles("*-IMPORT.txt");
                    LabelName = new List<string>();
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