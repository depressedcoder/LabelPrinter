using LabelPrinter.Model;
using System.Collections.Generic;

namespace LabelPrinter.Storage
{
    public abstract class AbstractStorage
    {
        public abstract void SaveLabel(Label label);
        public abstract List<string> GetLabelNames();
        public abstract Label GetLabel(string labelName);
        protected abstract string GetConnectionString();
    }
}
