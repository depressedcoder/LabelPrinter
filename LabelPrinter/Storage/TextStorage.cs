using LabelPrinter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LabelPrinter.Storage
{
    public class TextStorage : AbstractStorage
    {
        const string TextExtension = "-IMPORT.txt";

        public override List<string> GetLabelNames()
        {
            var directoryInfo = new DirectoryInfo(GetConnectionString());

            return directoryInfo
                .GetFiles($"*{TextExtension}")
                .Select(m => m.Name.Replace(TextExtension, ""))
                .ToList();
        }

        public override string[] GetLabelDetails(string labelName)
        {

            labelName += TextExtension;

            if (File.Exists(labelName))
            {
                var data = File.ReadAllLines(labelName);

                return data;

            }

            return new string[0];
        }

        public override void SaveLabel(string labelName, int howManyCoppies, IEnumerable<LabelRow> labelRows)
        {
            //Save all labels
            var fileName = $"{GetConnectionString()}{labelName}{TextExtension}";
            File.WriteAllText(fileName, labelName + Environment.NewLine +
                howManyCoppies + Environment.NewLine +
                string.Join(Environment.NewLine, labelRows.Select(m => m.Text).ToArray()));

            //Save metadata of labels
            var labelRowsMetadata = JsonConvert.SerializeObject(labelRows, Formatting.Indented);
            File.WriteAllText($"{GetConnectionString()}{labelName}.json", labelRowsMetadata);
        }

        public override List<string> GetLabelDetailsJson(string labelName)
        {
            //string jsonFile = labelName + ".json";
            //IList<LabelRow> rowInfo = JsonConvert.DeserializeObject<IList<LabelRow>>(File.ReadAllText(jsonFile));
            //return rowInfo;
            throw new NotImplementedException();
        }

        protected override string GetConnectionString()
        {
            if (!File.Exists("Config.json"))
                throw new ArgumentException("Configuration file is missing");

            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

            var connectionString = config?.TextConnection;

            if (!string.IsNullOrEmpty(connectionString))
                Directory.CreateDirectory(connectionString);

            return connectionString;
        }
    }
}
