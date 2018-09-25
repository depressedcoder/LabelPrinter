using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabelPrinter.Model
{
    public class PrintJobsLabel : ViewModelBase
    {
        List<PrintJobsRow> _printJobsRow;
        public List<PrintJobsRow> PrintJobsRow
        {
            get => _printJobsRow;
            set
            {
                _printJobsRow = value;
                RaisePropertyChanged(nameof(PrintJobsRow));
            }
        }
    }
}
