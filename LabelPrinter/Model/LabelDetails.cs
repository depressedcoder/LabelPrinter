using System.Collections.Generic;

namespace LabelPrinter.Model
{
    public class LabelDetails
    {
        public List<LabelRow> LabelRows { get; set; }
        public string Name { get; set; }
        public int NumberOfCopies { get; set; }
        public string SelectedBarcode { get; set; }
        public int BarcodeSize { get; set; }
        public int BarcodeWidth { get; set; }
        public bool IsAutomaticCuttingDevice { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int LeftDistance { get; set; }
    }
}