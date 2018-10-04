using Newtonsoft.Json;
using System.IO;
using LabelPrinter.Model;
using System;

namespace LabelPrinter.DatabaseWatcher
{
    public abstract class AbstractWatcher
    {
        #region Abstract Method(s)

        public abstract void NotifyNewItem();
        public abstract string GetConnectionString();

        #endregion

        #region Public method(s)

        public Label GetLabel(LabelIn labels)
        {
            string jsonData = string.Empty;
            string basePath = AppDomain.CurrentDomain.BaseDirectory + "Norsel.json";
            Label label = new Label();

            if (!File.Exists(basePath))
            {
                MessageView.Instance.ShowWarning("No template label is created named Norsel");
                return new Label();
            }
            else
            {
                jsonData = File.ReadAllText(basePath);
            }

            if (!string.IsNullOrEmpty(jsonData))
            {
                jsonData = jsonData.Replace("<I1>", labels.Line1);
                jsonData = jsonData.Replace("<I2>", labels.Line2);
                jsonData = jsonData.Replace("<I3>", labels.Line3);
                jsonData = jsonData.Replace("<I4>", labels.Line4);
                jsonData = jsonData.Replace("<I5>", labels.Line5);
                jsonData = jsonData.Replace("<I6>", labels.Line6);
                jsonData = jsonData.Replace("<I7>", labels.Line7);
                jsonData = jsonData.Replace("<I8>", labels.Line8);
                jsonData = jsonData.Replace("<I9>", labels.Line9);
                jsonData = jsonData.Replace("<I10>", labels.Line10);
                jsonData = jsonData.Replace("<I11>", labels.Line11);
                jsonData = jsonData.Replace("<I12>", labels.Line12);
                jsonData = jsonData.Replace("<I13>", labels.Line13);
                jsonData = jsonData.Replace("<I14>", labels.Line14);
                jsonData = jsonData.Replace("<I15>", labels.Line15);

                label = JsonConvert.DeserializeObject<Label>(jsonData);
            }
            return label;
        }

        #endregion
    }
}
