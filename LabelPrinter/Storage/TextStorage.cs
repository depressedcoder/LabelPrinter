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

        public override Label GetLabel(string labelName)
        {
            var fileNameOfLabel = $"{GetConnectionString()}{labelName}{TextExtension}";

            if (!File.Exists(fileNameOfLabel))
                return null;

            var labelJsonFile = $"{GetConnectionString()}{labelName}.json";

            if (!File.Exists(labelJsonFile))
                throw new ArgumentException("Invali  label name");

            var labelRowLines = File.ReadAllText(fileNameOfLabel).Split('\n');

            var label = JsonConvert.DeserializeObject<Label>(File.ReadAllText(labelJsonFile), new JsonSerializerSettings
            {
                Error = (sender, args) => { args.ErrorContext.Handled = true; }
            });

            if (label == null)
                return null;

            foreach (var labelRow in label.Rows)
            {
                var idx = label.Rows.IndexOf(labelRow) + 2; //First two lines are labelName & number of copies

                labelRow.Text = labelRowLines.ElementAtOrDefault(idx);
            }

            return label;
        }

        public override void SaveLabel(Label label)
        {
                        
                //Save all labels
                var fileName = $"{GetConnectionString()}{label.SelectedLabelName}{TextExtension}";
                File.WriteAllText(fileName, label.SelectedLabelName + '\n' +
                                            label.HowManyCoppies + '\n' +
                    string.Join("\n", label.Rows.Select(m => m.Text).ToArray()));

                //Save metadata of labels
                var labelRowsMetadata = JsonConvert.SerializeObject(label, Formatting.Indented);
                File.WriteAllText($"{GetConnectionString()}{label.SelectedLabelName}.json", labelRowsMetadata);
            
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

        public override string TestConnection(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
