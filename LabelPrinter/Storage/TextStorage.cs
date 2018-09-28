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
            string directory = GetConnectionString();
            var directoryInfo = new DirectoryInfo(directory);

            return directoryInfo
                .GetFiles($"*{TextExtension}")
                .Select(m => m.Name.Replace(TextExtension, ""))
                .ToList();
        }

        public override Label GetLabel(string labelName)
        {
            Label label = null;
            Labels labels = GetLabels(labelName);
            if (labels != null)
                label = JsonConvert.DeserializeObject<Label>(labels.Label);
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

        public override string GetConnectionString()
        {
            if (!File.Exists("Config.json"))
                throw new ArgumentException("Configuration file is missing");

            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));

            var connectionString = config?.TextConnection;

            if (!string.IsNullOrEmpty(connectionString))
                Directory.CreateDirectory(connectionString);

            return string.IsNullOrEmpty(connectionString) ?  AppDomain.CurrentDomain.BaseDirectory + "\\" : connectionString;
        }

        public override string TestConnection(string connectionString)
        {
            throw new NotImplementedException();
        }

        public override Labels GetLabels(string labelName)
        {
            Labels labels = new Labels();
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

            labels.Label = JsonConvert.SerializeObject(label);
            labels.Name = label.SelectedLabelName;
            labels.Wieght = label.LabelWidth;

            return labels;
        }

        public override bool IsDatabaseConnected(string connectionString)
        {
            // It returns always true because in case of test file not to connect database
            return true;
        }

        public override void DeleteLabel(Label label)
        {
            try
            {
                //delete all labels
                var fileName = $"{GetConnectionString()}{label.SelectedLabelName}{TextExtension}";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                //delete metadata of labels
                fileName = $"{GetConnectionString()}{label.SelectedLabelName}.json";
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                MessageView.Instance.ShowInformation("Delete Successful.");
            }
            catch (Exception ex)
            {
                MessageView.Instance.ShowError(ex.Message);
            }
        }
    }
}
