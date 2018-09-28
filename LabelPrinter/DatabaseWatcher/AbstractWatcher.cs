using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelPrinter.DatabaseWatcher
{
    public abstract class AbstractWatcher
    {
        public abstract void NotifyNewItem();
        public abstract string GetConnectionString();
    }
}
