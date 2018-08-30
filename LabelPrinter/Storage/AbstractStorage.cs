using LabelPrinter.Model;
using System;
using System.Collections.Generic;

namespace LabelPrinter.Storage
{
    public abstract class AbstractStorage
    {
        public string ConnectionName  { get; set; }

        public abstract void SaveLabel(string file,int howManyCoppies,IEnumerable<LabelRow> allLines);
        public abstract List<string> GetLabelNames();
        public abstract string[] GetLabelDetails(string labelName);
        public abstract List<string> GetLabelDetailsJson(string labelName);
    }
}
