using GalaSoft.MvvmLight;

namespace LabelPrinter.Model
{
    public class PrintJobsRow : ViewModelBase
    {
        private string _labelName;
        public string LabelName
        {
            get => _labelName;
            set
            {
                _labelName = value;
                RaisePropertyChanged(nameof(LabelName));
            }
        }

        private int _noOfCopy;
        public int NoOfCopy
        {
            get => _noOfCopy;
            set
            {
                _noOfCopy = value;
                RaisePropertyChanged(nameof(NoOfCopy));
            }
        }
    }
}
