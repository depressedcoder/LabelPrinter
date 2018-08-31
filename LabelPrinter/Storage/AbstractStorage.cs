using LabelPrinter.Model;
using System.Collections.Generic;

namespace LabelPrinter.Storage
{
    public abstract class AbstractStorage
    {
        public abstract void SaveLabel(string labelName, int howManyCoppies, IEnumerable<LabelRow> labelRows);
        public abstract List<string> GetLabelNames();
        public abstract string[] GetLabelDetails(string labelName);
        public abstract List<string> GetLabelDetailsJson(string labelName);

        protected abstract string GetConnectionString();
    }
}
