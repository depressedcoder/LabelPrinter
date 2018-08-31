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

        public override List<LabelRow> GetLabelDetails(string labelName)
        {
            var fileNameOfLabel = $"{GetConnectionString()}{labelName}{TextExtension}";

            if(!File.Exists(fileNameOfLabel))
                throw new ArgumentException($"{labelName} does not exist.");

            var labelMetadataFile = $"{GetConnectionString()}{labelName}.json";

            if(!File.Exists(labelMetadataFile))
                throw new ArgumentException("Invalid label name");

            var labelRowLines = File.ReadAllText(fileNameOfLabel).Split(Environment.NewLine.ToCharArray());

            var labelRows = JsonConvert.DeserializeObject<List<LabelRow>>(File.ReadAllText(labelMetadataFile));

            foreach (var labelRow in labelRows)
            {
                var idx = labelRows.IndexOf(labelRow);

                labelRow.Text = labelRowLines.ElementAtOrDefault(idx);
            }

            return labelRows;
        }

        public override void SaveLabel(string labelName, int numberOfCopies, IEnumerable<LabelRow> labelRows)
        {
            //Save all labels
            var fileName = $"{GetConnectionString()}{labelName}{TextExtension}";
            File.WriteAllText(fileName, labelName + Environment.NewLine +
                numberOfCopies + Environment.NewLine +
                string.Join(Environment.NewLine, labelRows.Select(m => m.Text).ToArray()));

            //Save metadata of labels
            var labelRowsMetadata = JsonConvert.SerializeObject(labelRows, Formatting.Indented);
            File.WriteAllText($"{GetConnectionString()}{labelName}.json", labelRowsMetadata);
        }

        protected override string GetConnectionString()
        {
            if (!File.Exists("Config.json"))
                throw new ArgumentException("Configuration file is missing");

            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

            var connectionString = config?.TextConnection;

            if (!string.IsNullOrEmpty(connectionString))
                Directory.CreateDirectory(connectionString);

            return connectionString ?? AppDomain.CurrentDomain.BaseDirectory + "\\";
        }
    }
}
