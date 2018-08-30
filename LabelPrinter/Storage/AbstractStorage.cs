using System.Collections.Generic;

namespace LabelPrinter.Storage
{
    public abstract class AbstractStorage
    {
        public string ConnectionName  { get; set; }

        public abstract void SaveLabel();
        public abstract List<string> GetLabels();
    }
}
