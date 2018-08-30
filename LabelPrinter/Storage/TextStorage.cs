using LabelPrinter.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace LabelPrinter.Storage
{
    public class TextStorage : AbstractStorage
    {
        public override void GetLabels()
        {
            DirectoryInfo d = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            FileInfo[] Files = d.GetFiles("*-IMPORT.txt");
            var m = new MainViewModel();
            m.LabelName = new List<string> { "" };

            foreach (FileInfo file in Files)
            {
                var item = file.Name;
                string trimmed = Regex.Replace(item, "-IMPORT.txt", "");
                m.LabelName.Add(trimmed);
            }
        }

        public override void SaveLabel()
        {
            throw new NotImplementedException();
        }
    }
}
