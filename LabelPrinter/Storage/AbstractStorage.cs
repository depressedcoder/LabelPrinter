using LabelPrinter.Model;
using System.Collections.Generic;

namespace LabelPrinter.Storage
{
    public abstract class AbstractStorage
    {
        public abstract void SaveLabel(string labelName, int numberOfCopies, IEnumerable<LabelRow> labelRows);
        public abstract List<string> GetLabelNames();
        public abstract List<LabelRow> GetLabelDetails(string labelName);
        protected abstract string GetConnectionString();
    }
}
