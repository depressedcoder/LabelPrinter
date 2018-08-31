using LabelPrinter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace LabelPrinter.Storage
{
    public class TextStorage : AbstractStorage
    {
        public override List<string> GetLabelNames()
        {
            DirectoryInfo d = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            FileInfo[] Files = d.GetFiles("*-IMPORT.txt");
            
            var LabelName = new List<string> { "" };

            foreach (FileInfo file in Files)
            {
                var item = file.Name;
                string trimmed = Regex.Replace(item, "-IMPORT.txt", "");
                LabelName.Add(trimmed);
            }
            return LabelName;
        }

        public override string[] GetLabelDetails(string labelName)
        {
            
            labelName += "-IMPORT.txt";

            if(File.Exists(labelName))
            {
                var data = File.ReadAllLines(labelName);

                return data;

            }
            
            return new string[0];
        }

        public override void SaveLabel(string ComBoxLabelName, int howManyCoppies, IEnumerable<LabelRow> allLines)
        {
            string fileName = ComBoxLabelName + "-IMPORT.txt";

            if (!File.Exists(fileName))
            {

                File.WriteAllText(fileName, ComBoxLabelName + Environment.NewLine +
                    howManyCoppies + Environment.NewLine +
                    string.Join(Environment.NewLine, allLines.Select(m => m.Text).ToArray()));

                var rowInfo = JsonConvert.SerializeObject(allLines);

                string trimmed = Regex.Replace(fileName, "-IMPORT.txt", "");

                trimmed += ".json";

                File.WriteAllText(trimmed, rowInfo);
                System.Windows.MessageBox.Show("File Saved Successfully");
            }
            else
            {
                System.Windows.MessageBox.Show(fileName + " exists!! Change the file name.");
            }
        }

        public override List<string> GetLabelDetailsJson(string labelName)
        {
            //string jsonFile = labelName + ".json";
            //IList<LabelRow> rowInfo = JsonConvert.DeserializeObject<IList<LabelRow>>(File.ReadAllText(jsonFile));
            //return rowInfo;
            throw new NotImplementedException();
        }
    }
}
